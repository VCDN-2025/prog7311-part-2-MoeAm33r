using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null) return NotFound();

            var products = await _context.Products
                .Where(p => p.FarmerId == farmer.Id)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

                if (farmer != null)
                {
                    product.FarmerId = farmer.Id;
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard");
                }
            }
            return View(product);
        }
    }
}