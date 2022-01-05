using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models
{
    public class Amenity
    {
        public int AmenityId { get; set; }

        [Display(Name = "Amenity Name")]
        [Required(ErrorMessage = "Enter Amenity name")]
        [MinLength(2, ErrorMessage = "Min 2 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        public string AmenityName { get; set; }
    }
}
