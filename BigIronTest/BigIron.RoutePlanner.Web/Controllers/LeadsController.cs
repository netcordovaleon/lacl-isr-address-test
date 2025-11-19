using BigIron.RoutePlanner.Application.DTOs;
using BigIron.RoutePlanner.Application.Services;
using BigIron.RoutePlanner.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigIron.RoutePlanner.Web.Controllers
{
    public class LeadsController : Controller
    {

        private readonly ILeadService _service;
        private readonly ICsvLeadParser _parser;
        private readonly IExport _exporter;

        private const double DEFAULT_LAT = 40.0;
        private const double DEFAULT_LNG = -95.0;

        public LeadsController(ILeadService service, ICsvLeadParser parser, IExport exporter)
        {
            _service = service;
            _parser = parser;
            _exporter = exporter;
        }

        [HttpGet]
        public IActionResult Index() => View();


        [HttpPost]
        public async Task<IActionResult> Index(UploadViewModel model)
        {
            if (model.LeadFile is null || model.LeadFile.Length == 0)
            {
                ModelState.AddModelError("", "CSV file is required.");
                return View(model);
            }

            using var reader = new StreamReader(model.LeadFile.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            var leads = _parser.Parse(content);

            await _service.UploadLeadsAsync(leads);

            if (model.HomeLat is null || model.HomeLng is null)
                return RedirectToAction("Summary");

            var route = await _service.GenerateRouteAsync(
                new RouteRequestDto(model.HomeLat.Value, model.HomeLng.Value));
            ViewBag.HomeLat = model.HomeLat.Value;
            ViewBag.HomeLng = model.HomeLng.Value;
            return View("Route", route);
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsv(double homeLat, double homeLng)
        {
            var sortedLeads = await _service.GenerateRouteAsync(
                new RouteRequestDto(homeLat, homeLng)
            );
            var bytes = _exporter.Export(sortedLeads);
            return File(bytes, "text/csv", "optimized-route.csv");
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var leads = await _service.GenerateRouteAsync(new RouteRequestDto(DEFAULT_LAT, DEFAULT_LNG));
            ViewBag.HomeLat = DEFAULT_LAT;
            ViewBag.HomeLng = DEFAULT_LNG;
            return View("Route", leads);
        }


    }
}
