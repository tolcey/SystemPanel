using Microsoft.AspNetCore.Mvc;
using SystemPanel.Services;

namespace SystemPanel.Controllers;

public class ServicesController : Controller
{
    private readonly IServiceStatusService _service;

    public ServicesController(IServiceStatusService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Status()
    {
        var services = await _service.GetAllServiceStatusesAsync();
        return View(services);
    }

    [HttpPost]
    public async Task<IActionResult> Start(int id)
    {
        await _service.StartServiceAsync(id);
        return RedirectToAction(nameof(Status));
    }

    [HttpPost]
    public async Task<IActionResult> Stop(int id)
    {
        await _service.StopServiceAsync(id);
        return RedirectToAction(nameof(Status));
    }
}
