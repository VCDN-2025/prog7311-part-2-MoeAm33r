using System.ComponentModel.DataAnnotations;
//Microsoft (2024) ASP.NET Core documentation. Available at:
//https://learn.microsoft.com/en-us/aspnet/core/
//(Accessed: 12 July 2024)
namespace AgriEnergyConnect.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }  // For redirecting after login
    }
}