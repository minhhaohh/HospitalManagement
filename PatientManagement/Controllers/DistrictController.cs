using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class DistrictController : Controller
    {
        private readonly ILogger<DistrictController> _logger;

        private readonly IGeographyService _geographyService;

        public DistrictController(ILogger<DistrictController> logger, IGeographyService geographyService)
        {
            _logger = logger;
            _geographyService = geographyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDataJqGrid(int page, int rows)
        {
            var data = await _geographyService.GetDistrictsForJqGridAsync(page, rows);

            return Json(data);
        }
    }
}
