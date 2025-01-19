using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class GpoController : Controller
{
    private readonly GpoService _gpoService;
    private readonly ILogger<GpoController> _logger;

    public GpoController(GpoService gpoService, ILogger<GpoController> logger)
    {
        _gpoService = gpoService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            var gpos = _gpoService.GetAllGpos();
            return View(gpos);  // GPO listesi view'a aktarılıyor
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching GPOs.");
            return View("Error", ex.Message);
        }
    }

    [HttpGet]
    public IActionResult CreateGpo()
    {
        return View();  // GPO oluşturma formu için View
    }

    [HttpPost]
    public IActionResult CreateGpo(string name, string description)
    {
        try
        {
            var result = _gpoService.CreateGpo(name, description);
            if (result)
            {
                _logger.LogInformation("GPO {Name} created successfully.", name);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "GPO oluşturulamadı");
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating GPO {Name}.", name);
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }

    [HttpPost]
    public IActionResult DeleteGpo(string gpoId)
    {
        try
        {
            var result = _gpoService.DeleteGpo(gpoId);
            if (result)
            {
                _logger.LogInformation("GPO {GpoId} deleted successfully.", gpoId);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "GPO silinemedi");
            return View("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting GPO {GpoId}.", gpoId);
            ModelState.AddModelError("", ex.Message);
            return View("Index");
        }
    }
}
