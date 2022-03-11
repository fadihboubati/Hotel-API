using Hotel_API.Data;
using Hotel_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces.Services
{
    public class HotelService : IHotel
    {
        private AppDbContext _context;
        private IHotelRoom _hotelRoomService;
        public HotelService(AppDbContext context, IHotelRoom hotelRoomService)
        {
            _context = context;
            _hotelRoomService = hotelRoomService;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task DeleteHotel(int? id)
        {
            try
            {
                Hotel hotel = await _context.Hotels.FindAsync(id);
                _context.Entry(hotel).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HotelDTO> GetHotel(int? id)
        {
            // First way
            Hotel hotel = await _context.Hotels.FindAsync(id);
            HotelDTO hotelDto = new HotelDTO
            {
                ID = (int)id,
                Name = hotel.HotelName,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.PhoneNumber

            };
            hotelDto.Rooms = await _hotelRoomService.GetHotelRooms(id);

            // Other way
            // where the id is equal to the id that was brought in as an argument
            var hotel2 = await _context.Hotels.Where(h => h.Id == id)
                                             .Include(h => h.HotelRooms)
                                             .ThenInclude(hr => hr.Room)
                                             .ThenInclude(r => r.RoomAmenities)
                                             .ThenInclude(ra => ra.Amenity)
                                             .FirstOrDefaultAsync();
            HotelDTO hotelDto2 = ConvertHotelIntoDTO(hotel);

            return hotelDto;
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            List<HotelDTO> hotelsDTOs = new();
            List<Hotel> hotels = await _context.Hotels.ToListAsync();
            foreach (var hotel in hotels)
            {
                HotelDTO hotelDto = await GetHotel(hotel.Id);
                hotelsDTOs.Add(hotelDto);
            }
            return hotelsDTOs;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Helper method that takes a hotel  
        /// and converts it into a hotelDto.
        /// </summary>
        /// <param name="hotel">A unique hotel object</param>
        /// <returns>A hotelRoomDto object</returns>
        private HotelDTO ConvertHotelIntoDTO(Hotel hotel)
        {
            HotelDTO hotelDto = new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.HotelName,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.PhoneNumber,
            };

            return hotelDto;
        }

    }
}
