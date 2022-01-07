using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Display(Name = "Hotel Name")]
        [Required(ErrorMessage = "Enter hotel's name")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        public string HotelName { get; set; }


        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Enter Street Address")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        public string StreetAddress { get; set; }


        [Required(ErrorMessage = "Enter the city")]
        [Display(Name = "City Name")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        public string City { get; set; }


        [Required(ErrorMessage = "Enter the state")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        [Display(Name = "State Name")]
        public string State { get; set; }


        [Required(ErrorMessage = "Enter the Country")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(50, ErrorMessage = "Max 50 character")]
        public string Country { get; set; }

        [Display(Name ="Phone number")]
        [Required(ErrorMessage = "Enter the phone number")]
        [MinLength(10, ErrorMessage ="Min 10 numberts")]
        [MaxLength(25, ErrorMessage ="Max 25 numberts")]
        public string PhoneNumber { get; set; }
    }
}
