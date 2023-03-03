using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.WEBUI.wwwroot.ViewModels;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.Controllers
{
    public class AccountController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            String UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(signInViewModel.UserName);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
                var result = await _signInManager.PasswordSignInAsync(appUser, signInViewModel.Password, true, true);

                if (result.Succeeded)
                {

                    string? redirect = Request.Query["returnUrl"];
                    if (string.IsNullOrWhiteSpace(redirect))
                        return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
            }
        showSameView:
            return View(signInViewModel);
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

    }
}
