using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string FarmName { get; set; }

        public string Region { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
