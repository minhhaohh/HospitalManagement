using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
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

        public async Task<JsonResult> GetData(int page, int rows)
        {
            var data = await _geographyService.GetProvincesForJqGridAsync(page, rows);

            return Json(data);
        }
    }
}
