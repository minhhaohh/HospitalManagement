using AutoMapper;
using CsvHelper;
using FluentValidation;
using Hospital.Domain.DTO;
using Hospital.Domain.Extensions;
using Hospital.Domain.Models;
using Hospital.Domain.Objects;
using Hospital.Web.Extensions;
using Hospital.Web.Models;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace Hospital.Controllers
{
    [Route("[controller]/[action]")]
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;

        private readonly IMapper _mapper;

        private readonly IGeographyService _geographyService;

        private readonly IPatientService _patientService;

        private readonly IValidator<PatientCreateDto> _validatorPatientCreate;

        private readonly IValidator<PatientUpdateDto> _validatorPatientUpdate;

        public PatientController(ILogger<PatientController> logger,
            IMapper mapper,
            IGeographyService geographyService,
            IPatientService patientService,
            IValidator<PatientCreateDto> validatorPatientCreate,
            IValidator<PatientUpdateDto> validatorPatientUpdate)
        {
            _logger = logger;
            _mapper = mapper;
            _geographyService = geographyService;
            _patientService = patientService;
            _validatorPatientCreate = validatorPatientCreate;
            _validatorPatientUpdate = validatorPatientUpdate;
        }

        [Route("/Patient")]
        public async Task<IActionResult> Index()
        {
            var patientFilterViewModel = new PatientFilterViewModel();

            var wards = (await _geographyService.GetWardsAsync());
            patientFilterViewModel.SelectWards = wards.ToSelectListItems(item => item.Code, item => item.Name);

            var districts = (await _geographyService.GetDistrictsAsync());
            patientFilterViewModel.SelectDistricts = districts.ToSelectListItems(item => item.Code, item => item.Name);

            var provinces = (await _geographyService.GetProvincesAsync());
            patientFilterViewModel.SelectProvinces = provinces.ToSelectListItems(item => item.Code, item => item.Name);

            return View(patientFilterViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> GetDataGrid(string sidx, string sord, int page, int rows, PatientQuery filter)
        {
            var paging = new PagingParams() { PageIndex = (page == 0 ? 0 : page - 1), PageSize = (rows == 0 ? 10 : rows) };
            var data = await _patientService.SearchPatientsAsync(filter, paging);

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> OpenModalDialog(bool isEdit = false, int id = 0)
        {
            var patientViewModel = new PatientViewModel();
            patientViewModel.IsEdit = isEdit;

            if (isEdit)
            {
               var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                    throw new Exception($"Not found Patient with Id {id}!!!");

                _mapper.Map(patient, patientViewModel);
            }

            var wards = (await _geographyService.GetWardsAsync());
            patientViewModel.SelectWards = wards.ToSelectListItems(item => item.Code, item => item.Name);

            var districts = (await _geographyService.GetDistrictsAsync());
            patientViewModel.SelectDistricts = districts.ToSelectListItems(item => item.Code, item => item.Name);

            var provinces = (await _geographyService.GetProvincesAsync());
            patientViewModel.SelectProvinces = provinces.ToSelectListItems(item => item.Code, item => item.Name);

            return PartialView("_PatientModalPartial", patientViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OpenDeleteConfirmModalDialog(int id, string chartNumber)
        {
            var patientDeleteConfirm = new PatientDeleteConfirmViewModel() { Id = id, ChartNumber = chartNumber };
            return PartialView("_PatientDeleteConfirmModal", patientDeleteConfirm);
        }

        [HttpPost]
        public async Task<JsonResult> Create(PatientCreateDto patient)
        {
            var validateResults = _validatorPatientCreate.Validate(patient);

            if (!validateResults.IsValid)
                return Json(validateResults.Errors.Select(x => x.ErrorMessage).ToList());

            var result = await _patientService.CreateAsync(patient);
            if (!result.IsEmpty())
                return Json(new List<string>() { result });

            return Json(new List<string>());
        }

        [HttpPut]
        public async Task<JsonResult> Update(int id, PatientUpdateDto patient)
        {
            var validateResults = _validatorPatientUpdate.Validate(patient);

            if (!validateResults.IsValid)
                return Json(validateResults.Errors.Select(x => x.ErrorMessage).ToList());

            var result = await _patientService.UpdateAsync(id, patient);
            if (!result.IsEmpty())
                return Json(new List<string>() { result });

            return Json(new List<string>());
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _patientService.DeleteAsync(id);
            if (!result.IsEmpty())
                return Json(new List<string>() { result });

            return Json(new List<string>());
        }

        [HttpPut]
        public async Task<JsonResult> UpdateProperty(int pk, string name, string value)
        {
            
            var result = await _patientService.UpdatePropertyAsync(pk, name, value);
            if (!result.IsEmpty())
                return Json(new List<string>() { result });

            return Json(new List<string>());
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel(PatientQuery filter)
        {
            var paging = new PagingParams() { PageIndex = 0, PageSize = 0 };
            var patients = await _patientService.SearchPatientsAsync(filter, paging);

            var result = WriteCsvToMemory(patients.Rows);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "application/octet-stream") { FileDownloadName = "patient.csv" };
        }

        public byte[] WriteCsvToMemory(List<PatientDto> records)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(records);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
