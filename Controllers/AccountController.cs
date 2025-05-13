using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//Microsoft (2024) ASP.NET Core documentation. Available at:
//https://learn.microsoft.com/en-us/aspnet/core/
//(Accessed: 12 July 2024)
namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, string role)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);

                    if (role == "Farmer")
                    {
                        _context.Farmers.Add(new Farmer
                        {
                            UserId = user.Id,
                            FarmName = model.FarmName,
                            Location = model.Location
                        });
                    }
                    else if (role == "Employee")
                    {
                        _context.Employees.Add(new Employee
                        {
                            UserId = user.Id,
                            Department = model.Department
                        });
                    }

                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}