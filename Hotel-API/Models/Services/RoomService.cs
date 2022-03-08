using Hotel_API.Data;
using Hotel_API.Models.DTOs;
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
        private IAmenity _amenityService;
        public RoomService(AppDbContext context, IAmenity amenityService)
        {
            _context = context;
            _amenityService = amenityService;
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
                RoomDTO hotel = await GetRoom(id);
                _context.Entry(hotel).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomDTO> GetRoom(int? id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            Layout layout = room.Layout; // ex: OneBedRoom

            string stringValue = layout.ToString(); // => "OneBedRoom"

            RoomDTO roomDTO = new RoomDTO
            {
                ID = room.Id,
                Name = room.RoomName,
                Layout = stringValue,
            };

            List<RoomAmenity> RoomAmenities = await _context.RoomAmenities
                .Where(ra => ra.RoomId == id)
                .ToListAsync();

            roomDTO.Amenities = new List<AmenityDTO>();
            foreach (var amenity in RoomAmenities)
            {
                roomDTO.Amenities.Add(await _amenityService.GetAmenity(amenity.AmenityId));
            }
            return roomDTO;


        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            List<Room> rooms =  await _context.Rooms.ToListAsync();
            List<RoomDTO> roomDTOs = new();

            foreach (var room in rooms)
            {
                RoomDTO roomDTO = new RoomDTO
                { 
                    ID = room.Id,
                    Name = room.RoomName
                };
            }

            return roomDTOs;
            // // Adding more details
            //return await _context.Rooms
            //    .Include(r => r.RoomAmenities)
            //    .ThenInclude(ra => ra.Amenity)
            //    .ToListAsync();
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
