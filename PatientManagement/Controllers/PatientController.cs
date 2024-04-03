using FluentValidation;
using FluentValidation.Results;
using Hospital.Domain.DTO;
using Hospital.Domain.Objects;
using Hospital.Domain.Validations;
using Hospital.Web.Models;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;

        private readonly IGeographyService _geographyService;

        private readonly IPatientService _patientService;

        private readonly IValidator<PatientCreateDto> _validatorPatientCreate;

        public PatientController(ILogger<PatientController> logger, 
            IGeographyService geographyService,
            IPatientService patientService,
            IValidator<PatientCreateDto> validatorPatientCreate)
        {
            _logger = logger;
            _geographyService = geographyService;
            _patientService = patientService;
            _validatorPatientCreate = validatorPatientCreate;
        }

        public async Task<IActionResult> Index()
        {
            var patientViewModel = new PatientViewModel();
            patientViewModel.ChartNumber = await _patientService.GetNewChartNumberAsync();

            var wards = (await _geographyService.GetWardsAsync());
            patientViewModel.Wards = wards.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var districts = (await _geographyService.GetDistrictsAsync());
            patientViewModel.Districts = districts.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            var provinces = (await _geographyService.GetProvincesAsync());
            patientViewModel.Provinces = provinces.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList();

            return View(patientViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> InitializeGrid(string sidx, string sord, int page, int rows)
        {
            var condition = new PatientQuery();
            var paging = new PagingParams() { PageIndex = page - 1, PageSize = rows };
            var data = await _patientService.SearchPatientsAsync(condition, paging);

            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetDataFilter(PatientQuery filter)
        {
            var paging = new PagingParams() { PageIndex = 0, PageSize = 10 };
            var data = await _patientService.SearchPatientsAsync(filter, paging);

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> Create(PatientCreateDto patient)
        {
            var results = _validatorPatientCreate.Validate(patient);

            if (!results.IsValid)
                return Json(results.Errors.Select(x => x.ErrorMessage).ToList());

            await _patientService.CreateAsync(patient);
            ModelState.Clear();

            return Json("");
        }
    }
}
