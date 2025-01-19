using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SystemPanel.Data;
using SystemPanel.Models;
using SystemPanel.Services;
using SystemPanel.ViewModels;

namespace SystemPanel.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDuyuruService _duyuruService;

        // Modify the constructor to pass the logger to the base class
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IDuyuruService duyuruService)
            : base(logger) // Passing the logger to the BaseController constructor
        {
            _logger = logger;
            _dbContext = dbContext;
            _duyuruService = duyuruService;
        }

        // Anasayfa (Index)
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index page accessed at {Time}", DateTime.UtcNow);

            // Duyuruları ve modülleri birleştirme
            var duyurular = await _duyuruService.GetAllAsync();
            var modules = _dbContext.Modules
                .OrderBy(m => m.Order)
                .ToList();

            // ViewData ile modülleri taşıma
            ViewData["Modules"] = modules;

            return View(duyurular); // Duyurular ana model
        }

        // Duyuru Detayları (PartialView)
        [HttpGet("/Details/{id}")]
        public async Task<IActionResult> DuyuruDetails(int id)
        {
            var duyuru = await _duyuruService.GetByIdAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return PartialView("_DuyuruDetails", duyuru);
        }

        // Privacy Sayfası
        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page accessed at {Time}", DateTime.UtcNow);
            return View();
        }

        // Hata Sayfası
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError("Error occurred with Request ID: {RequestId}", requestId);

            var model = new ErrorViewModel
            {
                RequestId = requestId
            };

            return View(model);
        }
    }
}
