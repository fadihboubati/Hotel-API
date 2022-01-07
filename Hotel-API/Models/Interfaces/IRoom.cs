using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IRoom
    {
       Task<Room> CreateRoom(Room room);
        Task<List<Room>> GetRooms();
        Task<Room> GetRoom(int? id);
        Task<Room> UpdateRoom(int? id, Room room);
        Task DeleteRoom(int? id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
