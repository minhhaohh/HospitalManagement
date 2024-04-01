using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace PatientManagement.Controllers
{
    public class WardController : Controller
    {
        private readonly ILogger<WardController> _logger;

        private readonly IGeographyService _geographyService;

        public WardController(ILogger<WardController> logger, IGeographyService geographyService)
        {
            _logger = logger;
            _geographyService = geographyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData(int page, int rows)
        {
            var wards = _geographyService.GetWards(page, rows);

            return Json(new { rows = wards });
        }
    }
}
