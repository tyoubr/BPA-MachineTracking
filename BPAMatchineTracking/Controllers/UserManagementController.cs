using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BPAMatchineTrack.Models.ViewModel;

[Authorize(Roles = "Super Admin")]
public class UserManagementController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManagementController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Show Users
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var model = new List<UserRoleViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            model.Add(new UserRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                CurrentRole = roles.FirstOrDefault(),
                Roles = _roleManager.Roles.Select(r => r.Name).ToList()
            });
        }

        return View(model);
    }

    // Update Role
    [HttpPost]
    public async Task<IActionResult> UpdateRole(UserRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user != null)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, model.SelectedRole);
        }

        return RedirectToAction("Index");
    }
}

