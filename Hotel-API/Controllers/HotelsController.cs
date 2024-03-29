﻿using System;
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
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotels = await _hotel.GetHotels();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HotelDTO hotel = await _hotel.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            try
            {
                if (id != hotel.Id)
                {
                    return BadRequest();
                }
                await _hotel.UpdateHotel(id, hotel);
                return Ok(hotel);
            }
            catch (Exception)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _hotel.Create(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _hotel.DeleteHotel(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        private async Task<bool> HotelExists(int? id)
        {
            if (id == null) return false;
            var hotels = await _hotel.GetHotels();
            return hotels.Any(e => e.ID == id);
        }
    }
}
