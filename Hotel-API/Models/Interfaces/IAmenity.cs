 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IAmenity
    {
        Task<Amenity> CreteAmenity(Amenity amenity);
        Task<List<Amenity>> GetAmenities();
        Task<Amenity> GetAmenity(int? id);
        Task<Amenity> UpdateAmenity(int? id, Amenity amenity);
        Task DeleteAmenity(int? id);

    }
}
