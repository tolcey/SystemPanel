using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using SystemPanel.Data;

namespace SystemPanel.Controllers
{
    [Route("api/map")]
    public class MapUnitsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        // Modify the constructor to pass the logger to the base class
        public MapUnitsController(ApplicationDbContext context, ILogger<MapUnitsController> logger)
            : base(logger)  // Passing the logger to the BaseController constructor
        {
            _context = context;
        }

        // Görünüm Yükleme: GET /MapUnits
        [Route("~/MapUnits")]
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Bu, Views/MapUnits/Index.cshtml dosyasını yükler
        }

        // Endpoint: GET /api/map/status
        [HttpGet("status")]
        public IActionResult GetCityStatuses()
        {
            var statuses = _context.MapUnits.Select(unit => new
            {
                city = unit.Name,
                switchStatus = "open", // Dinamik duruma göre düzenlenebilir
                dpServer = "closed",
                wds = "open"
            }).ToList();

            return Ok(statuses);
        }

        // Endpoint: GET /api/map/detail/{cityId}
        [HttpGet("detail/{cityId}")]
        public IActionResult GetCityDetail(int cityId)
        {
            var mapUnit = _context.MapUnits.FirstOrDefault(mu => mu.Id == cityId);
            if (mapUnit == null)
            {
                return NotFound(new { message = $"City with ID {cityId} not found." });
            }

            return Ok(new
            {
                city = mapUnit.Name,
                description = mapUnit.Description,
                devices = new[]
                {
                    new { name = "Switch-1", status = "open", ip = "192.168.1.1" },
                    new { name = "Crypto-Device-1", status = "closed", ip = "192.168.1.2" }
                },
                dpServer = "closed",
                wds = "open"
            });
        }

        // Endpoint: GET /api/map/cities
        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var cities = _context.MapUnits.Select(unit => new
            {
                id = unit.Id,
                city = unit.Name,
                latitude = unit.Latitude,
                longitude = unit.Longitude,
                description = unit.Description
            }).ToList();

            return Ok(cities);
        }
    }
}
