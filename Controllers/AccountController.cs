using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using SystemPanel.ViewModels.Account;

namespace SystemPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                if (model.IsAdUser)
                {
                    // AD Kullanıcı Doğrulama
                    using (var context = new PrincipalContext(ContextType.Domain, "YourDomainName"))
                    {
                        bool isValid = context.ValidateCredentials(model.Username, model.Password);
                        if (isValid)
                        {
                            // Kullanıcı giriş yaptıysa, yönlendirme yapılır
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "AD kullanıcı doğrulama başarısız.");
                        }
                    }
                }
                else
                {
                    // Normal Kullanıcı Doğrulama
                    var result = await _signInManager.PasswordSignInAsync(
                        model.Username,
                        model.Password,
                        model.RememberMe,
                        lockoutOnFailure: false
                    );

                    if (result.Succeeded)
                    {
                        return LocalRedirect(returnUrl ?? Url.Content("~/"));
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is locked. Please contact support.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
                    }
                }
            }

            return View(model);
        }

        // POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
