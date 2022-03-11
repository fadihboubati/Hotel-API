using Hotel_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// Creates a new entry in the Amenity database table,
        /// based on the AmenityDTO parameter.
        /// </summary>
        /// <param name="amenityDto">A unique AmenityDTO object</param>
        /// <returns>The created amenityDto object</returns>
        Task<AmenityDTO> CreteAmenity(AmenityDTO amenityDTO);

        /// <summary>
        /// Returns a list of all the Amenities in the Amenities database table,
        /// converted into AmenityDTOs objects.
        /// </summary>
        /// <returns>A list of all of the AmenityDtos</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Returns a specific Amenity from the Amenities database table,
        /// converted into an AmenityDto.
        /// </summary>
        /// <param name="id">A unique integer amenity ID value</param>
        /// <returns>A specific amenityDto object</returns>
        Task<AmenityDTO> GetAmenity(int? id);

        /// <summary>
        /// Updates a specific Amenity in the Amenity database,
        /// based on the amenityDto parameter.
        /// </summary>
        /// <param name="amenityDto">A unique amenityDto object</param>
        /// <returns>An updated amenityDto object</returns>
        Task<AmenityDTO> UpdateAmenity(int? id, AmenityDTO amenityDto);

        /// <summary>
        /// Deletes an Amenity from the Amenities database table,
        /// based on the Amenity Id parameter.
        /// </summary>
        /// <param name="id">A unique Amenity ID number</param>
        /// <returns>An empty task object</returns>
        Task DeleteAmenity(int? id);

    }
}
