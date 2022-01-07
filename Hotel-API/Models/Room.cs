using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Display(Name = "Room Name")]
        [Required(ErrorMessage = "Enter room's name")]
        [MinLength(3, ErrorMessage = "Min 3 character")]
        [MaxLength(14, ErrorMessage = "Max 14 character")]


        public string RoomName { get; set; }

        [Display(Name = "Layout Name")]
        [Required(ErrorMessage = "Enter room's name")]
        public Layout Layout { get; set; }

        [Display(Name = "Room Amenities")]
        public List<RoomAmenity> RoomAmenities { get; set; }
    }

    public enum Layout
    {
        Studio,
        OneBedRoom,
        TwoBedRoom
    }
}
