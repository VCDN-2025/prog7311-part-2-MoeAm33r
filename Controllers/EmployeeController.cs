using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
//Microsoft (2024) ASP.NET Core documentation. Available at:
//https://learn.microsoft.com/en-us/aspnet/core/
//(Accessed: 12 July 2024)
namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Employee/Dashboard
        public async Task<IActionResult> Dashboard(
            string categoryFilter,
            string farmerFilter,
            DateTime? startDate,
            DateTime? endDate)
        {
            // Base query with farmer inclusion
            var productsQuery = _context.Products
                .Include(p => p.Farmer)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                productsQuery = productsQuery.Where(p => p.Category == categoryFilter);
            }

            if (!string.IsNullOrEmpty(farmerFilter) && int.TryParse(farmerFilter, out int farmerId))
            {
                productsQuery = productsQuery.Where(p => p.FarmerId == farmerId);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate);
            }

            // Prepare view data
            ViewBag.Categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Farmers = await _context.Farmers.ToListAsync();

            return View(await productsQuery.ToListAsync());
        }

        // GET: /Employee/AddFarmer
        public IActionResult AddFarmer()
        {
            return View();
        }

        // POST: /Employee/AddFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(
            [Bind("FarmName,Location,ContactNumber")] Farmer farmer,
            string email,
            string password)
        {
            if (ModelState.IsValid)
            {
                // Create Identity User first
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Farmer");

                    // Link to farmer profile
                    farmer.UserId = user.Id;
                    _context.Add(farmer);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Dashboard));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(farmer);
        }

        // GET: /Employee/ProductReport
        public async Task<IActionResult> ProductReport()
        {
            var report = await _context.Products
                .Include(p => p.Farmer)
                .GroupBy(p => p.Category)
                .Select(g => new ProductCategoryReport
                {
                    Category = g.Key,
                    Count = g.Count(),
                    EarliestDate = g.Min(p => p.ProductionDate),
                    LatestDate = g.Max(p => p.ProductionDate)
                })
                .ToListAsync();

            return View(report);
        }

        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }

    public class ProductCategoryReport
    {
        public string Category { get; set; }
        public int Count { get; set; }
        public DateTime EarliestDate { get; set; }
        public DateTime LatestDate { get; set; }
    }
}