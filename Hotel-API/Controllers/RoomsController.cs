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
using Hotel_API.Models.DTOs;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _room.GetRoom(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int? id, RoomDTO roomDto)
        {
            try
            {
                if (id != roomDto.ID)
                {
                    return BadRequest();
                }

                await _room.UpdateRoom(id, roomDto);
                return Ok(roomDto);
            }
            catch (Exception)
            {
                if (!await RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO roomDto)
        {
            await _room.CreateRoom(roomDto);
            return CreatedAtAction("GetRoom", new { id = roomDto.ID }, roomDto);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _room.DeleteRoom(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            try
            {

                await _room.AddAmenityToRoom(roomId, amenityId);
                return NoContent();

            }
            catch (Exception exc)
            {
                if (exc is KeyNotFoundException)
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            await _room.RemoveAmentityFromRoom(roomId, amenityId);
            return NoContent();
        }

        private async Task<bool> RoomExists(int? id)
        {
            if (id == null) return false;
            var room = await _room.GetRooms();
            return room.Any(e => e.ID == id);
        }


    }
}
