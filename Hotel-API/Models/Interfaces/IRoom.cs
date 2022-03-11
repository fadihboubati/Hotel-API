using Hotel_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IRoom
    {
       Task<RoomDTO> CreateRoom(RoomDTO roomDto);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoom(int? id);
        Task<RoomDTO> UpdateRoom(int? id, RoomDTO roomDto);
        Task DeleteRoom(int? id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
