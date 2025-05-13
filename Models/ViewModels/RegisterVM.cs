using System.ComponentModel.DataAnnotations;
//Microsoft (2024) ASP.NET Core documentation. Available at:
//https://learn.microsoft.com/en-us/aspnet/core/
//(Accessed: 12 July 2024)
namespace AgriEnergyConnect.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Common fields
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        // Farmer-specific fields
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        // Employee-specific fields
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}