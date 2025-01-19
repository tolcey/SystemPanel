using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[Authorize]
public class BaseController : Controller
{
    private readonly ILogger<BaseController> _logger;

    // Dependency Injection ile logger'ı alıyoruz
    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    // Genel hata yönetimi için bir yöntem
    protected async Task<IActionResult> ExecuteWithLogging(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bir hata oluştu.");
            return View("Error"); // Hata sayfasına yönlendirilebilir.
        }
    }
}
