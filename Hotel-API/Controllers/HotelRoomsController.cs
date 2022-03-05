using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_API.Data;
using Hotel_API.Models;
using Hotel_API.Models.Interfaces;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        [HttpGet("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int? HotelId)
        {
            if (HotelId == null)
            {
                return NotFound();
            }
            var HotelRooms = await _hotelRoom.GetHotelRooms(HotelId);
            return HotelRooms;
        }

        [HttpGet("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int? HotelId, int? roomNumber)
        {
            if (HotelId == null || roomNumber == null)
            {
                return NotFound();
            }
            var hotelRoom = await _hotelRoom.GetHotelRoom(HotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        //PUT: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int HotelId, int RoomNumber, HotelRoom hotelRoom)
        {
            if (HotelId != hotelRoom.HotelId || RoomNumber != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }
            await _hotelRoom.UpdateHotelRoom(HotelId, RoomNumber, hotelRoom);
            return NoContent();
        }

        // POST to add a room to a hotel
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult> AddRoomToHotel(int? hotelId, HotelRoom hotelRoom)
        {
            if (hotelId == null || hotelRoom == null)
            {
                return NotFound();
            }

            try
            {
                await _hotelRoom.AddRoomToHotel(hotelId, hotelRoom);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        // DELETE a specific room from a hotel:
        //DELETE: api/HotelRooms/5
        [HttpDelete("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int HotelId, int roomNumber)
        {
            await _hotelRoom.Delete(HotelId, roomNumber);
            return NoContent();
        }

        //private bool HotelRoomExists(int id)
        //{
        //    return _context.HotelRooms.Any(e => e.HotelId == id);
        //}
    }
}
