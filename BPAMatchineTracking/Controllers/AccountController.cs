using BPAMatchineTrack.Models;
using BPAMatchineTrack.Models.ViewModel;
using BPAMatchineTrack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(string returnUrl="")
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        var model = new LoginViewModel()
        //        {
        //            ReturnUrl = returnUrl
        //        };

        //        //return LocalRedirect(returnUrl);
        //        return View(model);
        //    }

        //        return RedirectToAction("Index", "Home");
        //}


        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Check if roles exist; if not, create them (one time)
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new IdentityRole("User"));

                    // Assign role
                    var userCount = await _userManager.Users.CountAsync();
                    if (userCount == 1)
                    {
                        // First user → Admin
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        // All others → User
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    // Sign in the user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Display errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var rememberedEmail = Request.Cookies["RememberedEmail"];  // Get the remembered email from the cookie
            var rememberedPassword = Request.Cookies["RememberedPassword"];  // Get the remembered email from the cookie
            var rememberMeChecked = Request.Cookies["RememberedPassword"];  // Get the remembered email from the cookie

            // Pass the remembered email to the view (via ViewBag or ViewData)
            ViewData["RememberedEmail"] = rememberedEmail;
            ViewData["RememberedPassword"] = rememberedPassword;
            ViewData["RememberMeChecked"] = rememberMeChecked;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // If "Remember Me" is checked, save the email and password in cookies
                    if (model.RememberMe)
                    {
                        // Store email and password in cookies (note: insecure for passwords)
                        Response.Cookies.Append("RememberedEmail", model.Email, new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30), // Cookie expiry time (30 days)
                            IsEssential = true,
                            HttpOnly = true
                        });

                        Response.Cookies.Append("RememberedPassword", model.Password, new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30), // Cookie expiry time (30 days)
                            IsEssential = true,
                            HttpOnly = true
                        });
                        // Store the Remember Me state in a cookie as a string
                        Response.Cookies.Append("RememberMeChecked", "true", new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30), // Cookie expiry time (30 days)
                            IsEssential = true,
                            HttpOnly = true
                        });


                    }
                    else
                    {
                        // If "Remember Me" is not checked, remove the cookies
                        Response.Cookies.Delete("RememberedEmail");
                        Response.Cookies.Delete("RememberedPassword");
                        Response.Cookies.Delete("RememberMeChecked");
                    }

                    // Redirect after successful login
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    TempData["ErrorMessage"] = "Your account is locked out.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid login attempt.";
                    return RedirectToAction("Login");
                }
            }

            TempData["ErrorMessage"] = "Please provide valid login credentials.";
            return RedirectToAction("Login");
        }

        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(
        //            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            TempData["SuccessMessage"] = $"Login Successfully, {model.Email}!";
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else if (result.IsLockedOut)
        //        {
        //            TempData["ErrorMessage"] = "Your account is locked out.";
        //            return RedirectToAction("Login");
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Invalid login attempt.";
        //            return RedirectToAction("Login");
        //        }
        //    }

        //    // If ModelState is invalid
        //    TempData["ErrorMessage"] = "Please provide valid login credentials.";
        //    return RedirectToAction("Login");
        //}


        //[HttpPost("[controller]/Login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl="")
        //{
        //    try
        //    {

        //        if (ModelState.IsValid)
        //        {
        //            //var user = await _userManager.FindByEmailAsync(model.Email);
        //            ApplicationUser user;
        //            if (model.Email.Contains("@") && model.Email.Contains("."))
        //            {
        //                user = await _userManager.FindByEmailAsync(model.Email);
        //            }
        //            else
        //            {
        //                user = await _userManager.FindByNameAsync(model.Email);
        //            }

        //            if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
        //            {
        //                ModelState.AddModelError(string.Empty, "Email not confirmed yet");
        //                return View(model);
        //            }

        //            if (user != null)
        //            {
        //                var result = await _signInManager.PasswordSignInAsync(
        //                    user, model.Password, model.RememberMe, true);

        //                if (result.Succeeded)
        //                {
        //                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //                    {
        //                        return Redirect(returnUrl);
        //                    }

        //                    return RedirectToAction("Index", "Home");

        //                }

        //                // If account is lockedout
        //                if (result.IsLockedOut)
        //                {
        //                    return View("Account Locked");
        //                }
        //            }

        //            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        //        }

        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}


        // GET: /Account/ResetPassword
        [HttpGet]
        [Authorize(Roles = "Super Admin")]
        public IActionResult ResetPassword()
        {
            return View();
        }
        // POST: /Account/ResetPassword
        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ResetPassword"); // redirect so toaster shows
            }

            // Generate reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset password
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"Password for {model.Email} has been reset successfully!";
                return RedirectToAction("Login"); // redirect so toaster shows on login page
            }
            else
            {
                TempData["ErrorMessage"] = string.Join(", ", result.Errors.Select(e => e.Description));
                return RedirectToAction("ResetPassword");
            }
        }

        // POST: Account/Logout
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "Account"); // or any other page after logout
        }
    }
}
