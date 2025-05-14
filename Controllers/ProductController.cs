using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using System.Security.Claims;

namespace AgriEnergyConnect.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> MyProducts()
        {
            var user = await _userManager.GetUserAsync(User);
            var farmer = _context.Farmers.FirstOrDefault(f => f.FullName == user.UserName);
            var products = _context.Products
                .Where(p => p.FarmerId == farmer.Id)
                .Include(p => p.Farmer)
                .ToList();
            return View(products);
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Add(Product product)
        {
            var user = await _userManager.GetUserAsync(User);
            var farmer = _context.Farmers.FirstOrDefault(f => f.FullName == user.UserName);
            product.FarmerId = farmer.Id;

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("MyProducts");
            }
            return View(product);
        }

        [Authorize(Roles = "Employee")]
        public IActionResult ViewAll(string category, DateTime? fromDate, DateTime? toDate)
        {
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category.Contains(category));

            if (fromDate.HasValue)
                products = products.Where(p => p.ProductionDate >= fromDate.Value);

            if (toDate.HasValue)
                products = products.Where(p => p.ProductionDate <= toDate.Value);

            return View(products.ToList());
        }
    }
}

