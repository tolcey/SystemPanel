using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using SystemPanel.Hubs;
using SystemPanel.ViewModels; // ErrorViewModel'ı kullanabilmek için gerekli namespace

namespace SystemPanel.Controllers
{
    public class ServerMonitoringController : Controller
    {
        private readonly ServerMonitoringService _monitoringService;
        private readonly ILogger<ServerMonitoringController> _logger;
        private readonly IHubContext<MonitoringHub> _hubContext;

        public ServerMonitoringController(ServerMonitoringService monitoringService, ILogger<ServerMonitoringController> logger, IHubContext<MonitoringHub> hubContext)
        {
            _monitoringService = monitoringService;
            _logger = logger;
            _hubContext = hubContext;
        }

        // Sunucu metriklerini almak için Index aksiyonu
        public IActionResult Index()
        {
            try
            {
                var metrics = _monitoringService.GetServerMetrics();
                return View(metrics); // Varsayılan klasörde olduğu için yol belirtmeye gerek yok
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching server metrics.");
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error fetching server metrics",
                    Detail = ex.Message
                };
                return View("Error", errorViewModel); // Error view'ına yönlendirilir
            }
        }

        // SignalR üzerinden verileri push etmek için PushUpdates aksiyonu
        [HttpPost]
        public async Task<IActionResult> PushUpdates()
        {
            try
            {
                var metrics = _monitoringService.GetServerMetrics();
                await _hubContext.Clients.All.SendAsync("UpdateMetrics", metrics); // SignalR ile veri gönderimi
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error pushing updates.");
                return Json(new { success = false, message = "Error pushing updates", detail = ex.Message }); // JSON formatında hata mesajı
            }
        }

        // Sunucu oluşturma sayfasına yönlendiren GET aksiyonu
        [HttpGet]
        public IActionResult CreateServer()
        {
            return View();
        }

        // Sunucu ekleme işlemi için POST aksiyonu
        [HttpPost]
        public IActionResult AddServer(string serverName, string ipAddress)
        {
            try
            {
                var result = _monitoringService.AddServer(serverName, ipAddress);
                if (result)
                {
                    _logger.LogInformation("Server {ServerName} added successfully.", serverName);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Server eklenemedi");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding server {ServerName}.", serverName);
                ModelState.AddModelError("", ex.Message);
                return View(); // Eğer bir hata oluşursa, aynı sayfaya yönlendirilir
            }
        }

        // Sunucu durumunu görmek için Status sayfasını döndürür
        [HttpGet]
        public IActionResult Status()
        {
            return View();
        }
    }
}
