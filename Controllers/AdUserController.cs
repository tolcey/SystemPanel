using Microsoft.AspNetCore.Mvc;
using SystemPanel.Services;
using SystemPanel.ViewModels.User;

public class AdUserController : BaseController
{
    private readonly AdUserService _adUserService;

    public AdUserController(ILogger<AdUserController> logger, AdUserService adUserService)
        : base(logger)
    {
        _adUserService = adUserService;
    }

    // AD Kullanıcılarını Listeleme
    public async Task<IActionResult> Index()
    {
        return await ExecuteWithLogging(async () =>
        {
            var adUsers = await Task.Run(() => _adUserService.GetAllAdUsers());  // Wrap synchronous call in Task.Run
            return View(adUsers);
        });
    }
}
