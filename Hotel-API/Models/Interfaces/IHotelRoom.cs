using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IHotelRoom
    {
        //Task<HotelRoom> CreateHotelRoom(int HotelId, int RoomId);

        // PUT update the details of a specific room
        Task<HotelRoom> UpdateHotelRoom(int HotelId, int RoomNumber, HotelRoom hotelRoom);

        // DELETE a specific room from a hotel:
        Task Delete(int HotelId, int roomNumber);

        // GET all room details for a specific room
        Task<HotelRoom> GetHotelRoom(int? HotelId, int? RoomId);

        // GET all the rooms for a hotel: 
        Task<List<HotelRoom>> GetHotelRooms(int? HotelId);

        // POST to add a room to a hotel:
        Task AddRoomToHotel(int? HotelId, HotelRoom hotelRoom);
    }
}
