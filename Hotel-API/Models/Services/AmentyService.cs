﻿using Hotel_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces.Services
{
    public class AmentyService : IAmenity
    {
        private AppDbContext _context;
        public AmentyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity> CreteAmenity(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task DeleteAmenity(int? id)
        {
            try
            {
                Amenity hotel = await GetAmenity(id);
                _context.Entry(hotel).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            List<Amenity> amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetAmenity(int? id)
        {
            // // (using Ling)
            // // Note: you can remove the where, and add arrow function in the FirstOrDefaultAsync
            // // same as in the Rooms service
            return await _context.Amenities.Where(a => a.Id == id)
                .Include(a => a.RoomAmenities)
                .ThenInclude(ra => ra.Room)
                .FirstOrDefaultAsync();

            // // Explicit way (using extention)
            //Amenity amenity = await _context.Amenities.FindAsync(id);
            //List<RoomAmenity> roomAmenity = await _context.RoomAmenities.Where(a => a.AmenityId == id)
            //    .Include(a => a.Room)
            //    .ToListAsync();
            //amenity.RoomAmenities = roomAmenity;
            //return amenity;
        }

        public async Task<Amenity> UpdateAmenity(int? id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
