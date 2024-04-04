using Hospital.Domain.Objects;
using Hostpital.Service.IServices;
using Hostpital.Service.Services;
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
        public async Task<JsonResult> GetDataGrid(int page, int rows)
        {
            var paging = new PagingParams() { PageIndex = page - 1, PageSize = rows };

            var data = await _geographyService.SearchDistrictsAsync(paging);

            return Json(data);
        }
    }
}
