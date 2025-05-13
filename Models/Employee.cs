using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Links to Identity User

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        // Navigation property (optional)
        public IdentityUser User { get; set; }
    }
}