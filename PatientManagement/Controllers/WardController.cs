using Hospital.Domain.Objects;
using Hostpital.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("[controller]/[action]")]
    public class WardController : Controller
    {
        private readonly ILogger<WardController> _logger;

        private readonly IGeographyService _geographyService;

        public WardController(ILogger<WardController> logger, IGeographyService geographyService)
        {
            _logger = logger;
            _geographyService = geographyService;
        }

        [Route("/Ward")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDataGrid(int page, int rows)
        {
            var paging = new PagingParams() { PageIndex = page - 1, PageSize = rows };

            var data = await _geographyService.SearchWardsAsync(paging);

            return Json(data);
        }
    }
}
