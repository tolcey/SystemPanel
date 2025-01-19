using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;

public class IdentityUserController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityUserController(ILogger<IdentityUserController> logger, UserManager<IdentityUser> userManager)
        : base(logger)
    {
        _userManager = userManager;
    }

    // Veritabanı kullanıcılarını listeleme
    public async Task<IActionResult> Index()
    {
        return await ExecuteWithLogging(async () =>
        {
            var dbUsers = _userManager.Users.ToList();  // Identity kullanıcılarını al
            return View(dbUsers);
        });
    }
}
