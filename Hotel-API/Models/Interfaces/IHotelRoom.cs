using Hotel_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IHotelRoom
    {
        // PUT update the details of a specific room
        /// <summary>
        /// Update a hotelRoom object in the HotelRoom table in the database,
        /// based on the hhe HotelId, RoomNumber and the new hotelRoom object that want to updated
        /// </summary>
        /// <param name="HotelId">The unique HotelId used to update find the hotelId target</param>
        /// <param name="RoomNumber">The unique RoomNumber used to update find the hotelId target</param>
        /// <param name="hotelRoom">A unique hotelRoom object to updated</param>
        /// <returns>The HotelRoom object that was updated</returns>
        Task<HotelRoom> UpdateHotelRoom(int HotelId, int RoomNumber, HotelRoomDTO hotelRoomDto);

        // DELETE a specific room from a hotel:
        /// <summary>
        /// Delete a HotRoom object from the HotelRoom table in the database,
        /// based on the HotelId and roomnumber parameters
        /// </summary>
        /// <param name="HotelId">The unique HoterlId</param>
        /// <param name="roomNumber">The unique roomNumber</param>
        /// <returns>An empty task object</returns>
        Task Delete(int HotelId, int roomNumber);

        // GET all room details for a specific room
        /// <summary>
        /// Get a single HotelRoomns from the HotelRoom table in the database,
        /// based on the hotelId and Roomnumber  parameters.
        /// </summary>
        /// <param name="HotelId">The unique hotelId used to filtter the table</param>
        /// <param name="RoomId">The unique roomNumber used to filter the table</param>
        /// <returns>The single HotelRoom Object</returns>
        Task<HotelRoomDTO> GetHotelRoom(int? HotelId, int? RoomNumber);

        // GET all the rooms for a hotel: 
        /// <summary>
        /// Return a list of all the HotelRooms with the hotelId parameter.
        /// from the hotelRoom table in the databse
        /// </summary>
        /// <param name="HotelId">The unique hotelId used to filter the table</param>
        /// <returns>A list of all HotelRooms with a spacific hotelId</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int? HotelId);

        /// <summary>
        /// Creates a new HoterlRoom in the HotelRoom table in the database,
        /// based on the hotelRoom and the hoterlId parameters
        /// </summary>
        /// <param name="HotelId">The unique hotelId that want to add a room to it</param>
        /// <param name="hotelRoom">The hotelRoom obj that want to add it to the HotelRoom table</param>
        /// <returns>The hotelRoom object that was created/added</returns>
        Task AddRoomToHotel(int? HotelId, HotelRoomDTO hotelRoomDto);
    }
}
