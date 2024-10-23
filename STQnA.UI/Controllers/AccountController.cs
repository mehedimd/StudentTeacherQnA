using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using STQnA.Infrastructure;

namespace STQnA.UI.Controllers;

public class AccountController : Controller
{
    #region Config
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    #endregion

    #region Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Role = model.Role,
                Name = model.Role == "Student" ? model.Name : null,
                InstituteName = model.Role == "Student" ? model.InstituteName : null,
                InstituteIDCardNumber = model.Role == "Student" ? model.InstituteIDCardNumber : null
            };

            // Create the user using UserManager and hash the password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Optionally add the user to a specific role (Student/Teacher)
                await _userManager.AddToRoleAsync(user, model.Role);

                // Sign in the user (optional)
                //await _signInManager.SignInAsync(user, isPersistent: false);

                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }
    #endregion

    #region Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return View(model);
    }
    #endregion

    #region Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        // Redirect to the login page after logout
        return RedirectToAction("Login", "Account");
    }
    #endregion
}
