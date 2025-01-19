using Microsoft.AspNetCore.Mvc;
using SystemPanel.Services;

namespace SystemPanel.Api.Controllers;

[Route("api/services")]
public class ApiServicesController : ControllerBase
{
    private readonly IServiceStatusService _serviceStatusService;

    public ApiServicesController(IServiceStatusService serviceStatusService)
    {
        _serviceStatusService = serviceStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var services = await _serviceStatusService.GetAllServiceStatusesAsync();
        return Ok(services);
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartService(int id)
    {
        await _serviceStatusService.StartServiceAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/stop")]
    public async Task<IActionResult> StopService(int id)
    {
        await _serviceStatusService.StopServiceAsync(id);
        return NoContent();
    }
}
