using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, StringLength(100)]
        public string FarmName { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }

        [Phone, StringLength(20)]
        public string ContactNumber { get; set; }
    }
}