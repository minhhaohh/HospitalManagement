using FluentValidation.Results;
using Hospital.Domain.DTO;
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

        public PatientController(ILogger<PatientController> logger, 
            IGeographyService geographyService,
            IPatientService patientService)
        {
            _logger = logger;
            _geographyService = geographyService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var patientViewModel = new PatientViewModel();
            patientViewModel.ChartNumber = await _patientService.GetNewChartNumberAsync();

            var wards = (await _geographyService.GetWardsAsync());
            patientViewModel.Wards =
            [
                new SelectListItem() { Value = string.Empty, Text = string.Empty },
                .. wards.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList(),
            ];

            var districts = (await _geographyService.GetDistrictsAsync());
            patientViewModel.Districts =
            [
                new SelectListItem() { Value = string.Empty, Text = string.Empty },
                .. districts.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList(),
            ];

            var provinces = (await _geographyService.GetProvincesAsync());
            patientViewModel.Provinces =
            [
                new SelectListItem() { Value = string.Empty, Text = string.Empty },
                .. provinces.Select(x => new SelectListItem { Value = x.Code, Text = x.Name }).ToList(),
            ];

            return View(patientViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(int page, int rows)
        {
            var data = await _patientService.GetPatientsForJqGridAsync(page, rows);

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> Create(PatientCreateDto patient)
        {
            PatientValidator validator = new PatientValidator();
            ValidationResult results = validator.Validate(patient);

            if (!results.IsValid)
                return Json(results.Errors[0].ErrorMessage);

            await _patientService.CreateAsync(patient);

            return Json("");
        }
    }
}
