using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Data
{
    public static class SeedData
    {
        public static async Task Initialize(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            // Seed roles
            string[] roleNames = { "Farmer", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed admin user
            var adminEmail = "admin@agrienergy.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Employee");

                context.Employees.Add(new Employee
                {
                    UserId = adminUser.Id,
                    Department = "Administration"
                });
                await context.SaveChangesAsync();
            }
        }
    }
}