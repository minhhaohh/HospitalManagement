using Microsoft.AspNetCore.Mvc;
using PatientManagement.IServices;

namespace PatientManagement.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly ILogger<ProvinceController> _logger;

        private readonly IGeographyService _geographyService;

        public ProvinceController(ILogger<ProvinceController> logger, IGeographyService geographyService)
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
            var provinces = _geographyService.GetProvinces(page, rows);

            return Json(new { rows = provinces });
        }
    }
}
