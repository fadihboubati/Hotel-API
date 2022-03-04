using Hotel_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces.Services
{
    public class RoomService : IRoom
    {
        private AppDbContext _context;
        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoom(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoom(int? id)
        {
            try
            {
                Room hotel = await GetRoom(id);
                _context.Entry(hotel).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Room> GetRoom(int? id)
        {
            return await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .ToListAsync();
        }

        public async Task<Room> UpdateRoom(int? id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            var result = await _context.RoomAmenities
                .FirstOrDefaultAsync(ra => ra.RoomId == roomId && ra.AmenityId == amenityId);

            if (result == null)
            {
                RoomAmenity roomAmenity = new RoomAmenity
                {
                    RoomId = roomId,
                    AmenityId = amenityId
                };

                _context.Entry(roomAmenity).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Amenity already exist
                throw new KeyNotFoundException();
            }
        }

        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var result = await _context.RoomAmenities
                .FirstOrDefaultAsync(ra => ra.RoomId == roomId && ra.AmenityId == amenityId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }


    }
}
