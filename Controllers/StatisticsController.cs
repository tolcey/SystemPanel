using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SystemPanel.Services;

namespace systempanelmodern.Controllers;

public class StatisticsController : Controller
{
    private readonly IStatisticsService _service;

    public StatisticsController(IStatisticsService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Details()
    {
        var statistics = await _service.GetStatisticsAsync();
        return View(statistics);
    }
}
