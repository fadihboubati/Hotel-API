using Hotel_API.Data;
using Hotel_API.Models.DTOs;
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

        public async Task<AmenityDTO> CreteAmenity(AmenityDTO amenityDto)
        {
            Amenity amenity = new Amenity
            {
                Id = amenityDto.ID,
                AmenityName = amenityDto.Name
            };

            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenityDto;
        }

        public async Task DeleteAmenity(int? id)
        {
            try
            {
                Amenity amenity = await _context.Amenities.Where(a => a.Id == id)
                                .FirstOrDefaultAsync();
                _context.Entry(amenity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            List<Amenity> amenities = await _context.Amenities.ToListAsync();
            List<AmenityDTO> amenityDTOs = new();
            foreach (var item in amenities)
            {
                AmenityDTO amenityDTO = new AmenityDTO
                {
                    ID = item.Id,
                    Name = item.AmenityName
                };

                amenityDTOs.Add(amenityDTO);
            }

            return amenityDTOs;
        }

        public async Task<AmenityDTO> GetAmenity(int? id)
        {
            Amenity amenity = await _context.Amenities.Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return new AmenityDTO { ID = amenity.Id, Name = amenity.AmenityName };
        }

        public async Task<AmenityDTO> UpdateAmenity(int? id, AmenityDTO amenityDto)
        {
            Amenity amenity = new Amenity { Id = amenityDto.ID, AmenityName = amenityDto.Name };
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenityDto;
        }
    }
}
