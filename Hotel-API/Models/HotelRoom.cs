using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models
{
    public class HotelRoom
    {
        // One Hotel has many HotelRoom (Convention 4)
        public int HotelId { get; set; } // FK, CK
        public Hotel Hotel { get; set; }


        public int RoomId { get; set; } // FK
        public Room Room { get; set; }


        public int RoomNumber { get; set; } // CK

        public decimal Rate { get; set; }
        public bool PitFriendly { get; set; } 

    }
}
