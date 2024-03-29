﻿using Hotel_API.Data;
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

        public async Task<RoomDTO> CreateRoom(RoomDTO roomDto)
        {
            // The only difference between the update and the creation is that is in the update we need the ID for updating
            Room room = new Room
            {
                RoomName = roomDto.Name,
                Layout = (Layout)Enum.Parse(typeof(Layout), roomDto.Layout) // String to enum
            };
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            roomDto.ID = room.Id; // After save

            return roomDto;
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

            Layout layout = room.Layout; // ex: OneBedRoom, datatype: Layout
            string stringValue = layout.ToString(); // => "OneBedRoom", Enum to string, datatype: string

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
                AmenityDTO amenityDto = await _amenityService.GetAmenity(amenity.AmenityId);
                roomDTO.Amenities.Add(amenityDto);
            }
            return roomDTO;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            List<RoomDTO> roomsDTOs = new();
            List<Room> rooms =  await _context.Rooms.ToListAsync();

            foreach (var room in rooms)
            {
                RoomDTO roomDto = await GetRoom(room.Id);
                roomsDTOs.Add(roomDto);
            }

            return roomsDTOs;
        }

        public async Task<RoomDTO> UpdateRoom(int? id, RoomDTO roomDto)
        {
            // The only difference between the update and the creation is that is in the update we need the ID for updating
            Room room = new Room
            {
                Id = roomDto.ID,
                RoomName = roomDto.Name,
                Layout = (Layout)Enum.Parse(typeof(Layout), roomDto.Layout) // String to enum
            };

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDto;
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
