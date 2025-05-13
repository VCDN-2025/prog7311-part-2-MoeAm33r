using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional configurations
        }
    }
} //Microsoft (2024) ASP.NET Core documentation. Available at:
  //https://learn.microsoft.com/en-us/aspnet/core/
  //(Accessed: 12 July 2024)