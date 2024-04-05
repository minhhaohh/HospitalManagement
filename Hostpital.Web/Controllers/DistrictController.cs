using Hospital.Domain.Objects;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("[controller]/[action]")]
    public class DistrictController : Controller
    {
        private readonly ILogger<DistrictController> _logger;

        private readonly IGeographyService _geographyService;

        public DistrictController(ILogger<DistrictController> logger, IGeographyService geographyService)
        {
            _logger = logger;
            _geographyService = geographyService;
        }

        [Route("/District")]
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
