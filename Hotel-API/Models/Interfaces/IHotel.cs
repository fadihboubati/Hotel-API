﻿using Hotel_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);
        Task<List<HotelDTO>> GetHotels();
        Task<HotelDTO> GetHotel(int? id);
        Task<Hotel> UpdateHotel(int id, Hotel hotel);
        Task DeleteHotel(int? id);
    }
}
