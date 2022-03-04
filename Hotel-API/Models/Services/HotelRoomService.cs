using Hotel_API.Data;
using Hotel_API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private AppDbContext  _context { get; set; }
        public HotelRoomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int HotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr => hr.HotelId == HotelId && hr.RoomNumber == roomNumber);

            if (hotelRoom == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> GetHotelRoom(int? HotelId, int? RoomNumber)
        {
            return await _context.HotelRooms
            .Where(h => h.HotelId == HotelId)
            .Include(h => h.Room)
            .ThenInclude(r => r.RoomAmenities)
            .ThenInclude(ra => ra.Amenity)
            .FirstOrDefaultAsync(hr => hr.HotelId == HotelId && hr.RoomNumber == RoomNumber);
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int? HotelId)
        {
            return await _context.HotelRooms
                .Where(h => h.HotelId == HotelId)
                .Include(h => h.Room)
                .ToListAsync();

            //return await _context.Rooms
            //    .Include(r => r.HotelRooms)
            //    .ThenInclude(hr => hr.HotelId == HotelId)
            //    .ToListAsync();


            //return await _context.Hotels
            //    .Include(x => x.HotelRooms)
            //    .ThenInclude(hr => hr.Room)
            //    .ToListAsync();



            throw new NotImplementedException();
        }

        public async Task<HotelRoom> UpdateHotelRoom(int HotelId, int RoomNumber, HotelRoom hotelRoom)
        {
            var result = _context.HotelRooms.Where(hr => hr.HotelId == HotelId && hr.RoomNumber == RoomNumber);

            if (result != null)
            {
                _context.Entry(hotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return hotelRoom;
            }
            else
            {
                throw new NotImplementedException();

            }

        }

        public async Task AddRoomToHotel(int? HotelId , HotelRoom hotelRoom)
        {
            Hotel hotel = await _context.Hotels.FindAsync(HotelId);

            if (hotel == null)
            {
                throw new KeyNotFoundException();
            };

            HotelRoom HotelRoom = new HotelRoom
            {
                HotelId = (int)HotelId,
                RoomId = hotelRoom.RoomId,
                Rate = hotelRoom.Rate,
                PitFriendly = hotelRoom.PitFriendly,
                RoomNumber = hotelRoom.RoomNumber,

                //Hotel = hotel,
                //Room = hotelRoom.Room
            };

            _context.Entry(HotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

        }
    }
}
