using FluentValidation;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Domain.Objects;
using Hospital.Web.Models;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;

        private readonly IGeographyService _geographyService;

        private readonly IPatientService _patientService;

        private readonly IValidator<PatientCreateDto> _validatorPatientCreate;

        private readonly IValidator<PatientUpdateDto> _validatorPatientUpdate;

        public PatientController(ILogger<PatientController> logger, 
            IGeographyService geographyService,
            IPatientService patientService,
            IValidator<PatientCreateDto> validatorPatientCreate,
            IValidator<PatientUpdateDto> validatorPatientUpdate)
        {
            _logger = logger;
            _geographyService = geographyService;
            _patientService = patientService;
            _validatorPatientCreate = validatorPatientCreate;
            _validatorPatientUpdate = validatorPatientUpdate;
        }

        public async Task<IActionResult> Index()
        {
            var patientFilterViewModel = new PatientFilterViewModel();

            var wards = (await _geographyService.GetWardsAsync());
            patientFilterViewModel.SelectListWards = wards.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var districts = (await _geographyService.GetDistrictsAsync());
            patientFilterViewModel.SelectListDistricts = districts.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var provinces = (await _geographyService.GetProvincesAsync());
            patientFilterViewModel.SelectListProvinces = provinces.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            return View(patientFilterViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetDataGrid(string sidx, string sord, int page, int rows, PatientQuery filter)
        {
            var paging = new PagingParams() { PageIndex = (page == 0 ? 0 : page - 1), PageSize = (rows == 0 ? 10 : rows) };
            var data = await _patientService.SearchPatientsAsync(filter, paging);

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> OpenModalDialog(bool isEdit = false, string chartNumber = "")
        {
            var patientViewModel = new PatientViewModel();
            patientViewModel.IsEdit = isEdit;

            if (isEdit)
            {
                if (string.IsNullOrEmpty(chartNumber))
                    throw new ArgumentException("Not found ChartNumber!!!");

                var patient = await _patientService.GetPatientByChartNumberAsync(chartNumber);
                if (patient == null)
                    throw new ArgumentException($"Not found Patient with ChartNumber {chartNumber}!!!");

                patientViewModel.ChartNumber = chartNumber;
                patientViewModel.FirstName = patient.FirstName;
                patientViewModel.LastName = patient.LastName;
                patientViewModel.Gender = patient.Gender;
                patientViewModel.Dob = patient.Dob; 
                patientViewModel.Phone = patient.Phone;
                patientViewModel.Email = patient.Email;
                patientViewModel.Address = patient.Address;
                patientViewModel.WardCode = patient.WardCode;
                patientViewModel.DistrictCode = patient.DistrictCode;
                patientViewModel.ProvinceCode = patient.ProvinceCode;
                patientViewModel.ZipCode = patient.ZipCode;
            }
            else
            {
                patientViewModel.ChartNumber = await _patientService.GetNewChartNumberAsync();
            }

            var wards = (await _geographyService.GetWardsAsync());
            patientViewModel.SelectListWards = wards.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var districts = (await _geographyService.GetDistrictsAsync());
            patientViewModel.SelectListDistricts = districts.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var provinces = (await _geographyService.GetProvincesAsync());
            patientViewModel.SelectListProvinces = provinces.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            return PartialView("_PatientModalPartial", patientViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Create(PatientCreateDto patient)
        {
            var validateResults = _validatorPatientCreate.Validate(patient);

            if (!validateResults.IsValid)
                return Json(validateResults.Errors.Select(x => x.ErrorMessage).ToList());

            var result = await _patientService.CreateAsync(patient);
            if (!result)
                return Json(new List<string>() { "System Error!!!" });

            return Json(new List<string>());
        }

        [HttpPut]
        public async Task<JsonResult> Update(string chartNumber, PatientUpdateDto patient)
        {
            var validateResults = _validatorPatientUpdate.Validate(patient);

            if (!validateResults.IsValid)
                return Json(validateResults.Errors.Select(x => x.ErrorMessage).ToList());

            var result = await _patientService.UpdateAsync(chartNumber, patient);
            if (!result)
                return Json(new List<string>() { "System Error!!!" });

            return Json(new List<string>());
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string chartNumber)
        {
            var result = await _patientService.DeleteAsync(chartNumber);
            if (!result)
                return Json(new List<string>() { "System Error!!!" });

            return Json(new List<string>());
        }
    }
}
