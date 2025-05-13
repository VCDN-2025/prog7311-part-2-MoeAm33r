using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//Microsoft (2024) ASP.NET Core documentation. Available at:
//https://learn.microsoft.com/en-us/aspnet/core/
//(Accessed: 12 July 2024)
namespace AgriEnergyConnect.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Farmer"))
                {
                    return RedirectToAction("Dashboard", "Farmer");
                }
                else if (User.IsInRole("Employee"))
                {
                    return RedirectToAction("Dashboard", "Employee");
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
