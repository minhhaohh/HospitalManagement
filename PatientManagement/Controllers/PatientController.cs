using Hospital.Domain.DTO;
using Hospital.Web.Models;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            var patientViewModel = new PatientViewModel();
            patientViewModel.Wards = _geographyService.GetWards(1, 10).rows;
            patientViewModel.Districts = _geographyService.GetDistricts(1, 10).rows;
            patientViewModel.Provinces = _geographyService.GetProvinces(1, 10).rows;
            return View(patientViewModel);
        }

        [HttpPost]
        public IActionResult Create(PatientCreateDto patient)
        {
            _patientService.Create(patient);
            return View();
        }
    }
}
