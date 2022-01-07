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

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity  _amenity;

        public AmenitiesController(IAmenity amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenities()
        {
            var Amenities = await _amenity.GetAmenities();
            return Ok(Amenities);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _amenity.GetAmenity(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int? id, Amenity amenity)
        {
            try
            {
                if (id != amenity.Id)
                {
                    return BadRequest();
                }
                await _amenity.UpdateAmenity(id, amenity);
                return Ok(amenity);
            }
            catch (Exception)
            {
                if (!await AmenityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            await _amenity.CreteAmenity(amenity);
            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _amenity.DeleteAmenity(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        private async Task<bool> AmenityExists(int? id)
        {
            if (id == null) return false;
            var amemities = await _amenity.GetAmenities();
            return amemities.Any(e => e.Id == id);
        }
    }
}
