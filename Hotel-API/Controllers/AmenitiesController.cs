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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity  _amenity;

        public AmenitiesController(IAmenity amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            var Amenities = await _amenity.GetAmenities();
            return Ok(Amenities);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var AmenityDto = await _amenity.GetAmenity(id);
                return Ok(AmenityDto);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int? id, AmenityDTO amenityDto)
        {
            try
            {
                if (id != amenityDto.ID)
                {
                    return BadRequest();
                }
                await _amenity.UpdateAmenity(id, amenityDto);
                return Ok(amenityDto);
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
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenityDto)
        {
            await _amenity.CreteAmenity(amenityDto);
            return CreatedAtAction("GetAmenity", new { id = amenityDto.ID }, amenityDto);
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
            return amemities.Any(e => e.ID == id);
        }
    }
}
