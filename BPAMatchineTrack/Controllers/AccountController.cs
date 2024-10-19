using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl="")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                var model = new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                };

                //return LocalRedirect(returnUrl);
                return View(model);
            }

                return RedirectToAction("Index", "Home");
        }
        [HttpPost("[controller]/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl="")
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //var user = await _userManager.FindByEmailAsync(model.Email);
                    ApplicationUser user;
                    if (model.Email.Contains("@") && model.Email.Contains("."))
                    {
                        user = await _userManager.FindByEmailAsync(model.Email);
                    }
                    else
                    {
                        user = await _userManager.FindByNameAsync(model.Email);
                    }

                    if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                    {
                        ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                        return View(model);
                    }

                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(
                            user, model.Password, model.RememberMe, true);

                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }

                            return RedirectToAction("Index", "Home");

                        }

                        // If account is lockedout
                        if (result.IsLockedOut)
                        {
                            return View("Account Locked");
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
