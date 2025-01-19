using Microsoft.AspNetCore.Mvc;
using SystemPanel.Models;
using SystemPanel.Services;

namespace SystemPanel.Controllers
{
    [Route("Sccm")]
    public class SccmController : Controller
    {
        private readonly SccmLogService _sccmLogService;

        public SccmController(SccmLogService sccmLogService)
        {
            _sccmLogService = sccmLogService;
        }

        // Distribution Points ve Servisler için Action
        [HttpGet("dps")]
        public IActionResult Dps()
        {
            // SCCM Log Service ile dağıtım noktalarını ve servisleri getir.
            var dps = _sccmLogService.FetchDistributionPointsAndServices();
            return View(dps); // Görünüm olarak döndür.
        }

        // Sistem Durumu için Action (AJAX çağrıları için)
        [HttpGet("SystemStatus")]
        public IActionResult SystemStatus()
        {
            // Örnek veri: Gerçek bir servisten veya veri tabanından veri çekilebilir.
            var systemStatus = new
            {
                ActiveServers = 4,
                RunningServices = 10,
                StoppedServices = 2
            };

            return Json(systemStatus); // JSON formatında döndür.
        }
    }
}
