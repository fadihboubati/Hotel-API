using Hotel_API.Data;
using Hotel_API.Models.DTOs;
using Hotel_API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private AppDbContext _context { get; set; }
        private IRoom _roomService { get; set; }
        public HotelRoomService(AppDbContext context, IRoom roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber);
            // // Or
            //HotelRoom hotelRoom = await GetHotelRoom(HotelId, roomNumber);

            if (hotelRoom == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        public async Task<HotelRoomDTO> GetHotelRoom(int? hotelId, int? roomNumber)
        {
            var hotelRooms = await _context.HotelRooms.Where(h => h.HotelId == hotelId && h.RoomNumber == roomNumber)
            .FirstOrDefaultAsync();

            HotelRoomDTO hotelRoomDTO = ConvertEntityIntoDTO(hotelRooms);

            hotelRoomDTO.Room = await _roomService.GetRoom(hotelRooms.RoomId);
            return hotelRoomDTO;

        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int? hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                .ToListAsync();

            List<HotelRoomDTO> hotelRoosmDTOs = new List<HotelRoomDTO>();
            foreach (var item in hotelRooms)
            {
                HotelRoomDTO hotelDto = await GetHotelRoom(hotelId, item.RoomNumber);
                hotelRoosmDTOs.Add(hotelDto);
            }

            return hotelRoosmDTOs;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int HotelId, int RoomNumber, HotelRoomDTO hotelRoomDto)
        {
            var result = _context.HotelRooms.Where(hr => hr.HotelId == HotelId && hr.RoomNumber == RoomNumber);

            if (result != null)
            {
                HotelRoom hotelRoom = ConvertDTOIntoEntity(hotelRoomDto);

                _context.Entry(hotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return hotelRoom;
            }
            else
            {
                throw new NotImplementedException();

            }

        }

        public async Task AddRoomToHotel(int? HotelId , HotelRoomDTO hotelRoomDto)
        {
            Hotel hotel = await _context.Hotels.FindAsync(HotelId);

            if (hotel == null)
            {
                throw new KeyNotFoundException();
            };

            HotelRoom hotelRoom = ConvertDTOIntoEntity(hotelRoomDto);

            hotelRoom.HotelId = (int)HotelId;
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

        }

        // // Helper methods

        /// <summary>
        /// Helper method that takes a hotelRoomDto 
        /// and converts it into a hotelRoom entity.
        /// </summary>
        /// <param name="hotelRoomDto">A unique hotelRoomDto object</param>
        /// <returns>A hotelRoom object</returns>
        private HotelRoom ConvertDTOIntoEntity(HotelRoomDTO hotelRoomDto)
        {

            HotelRoom hotelRoom = new HotelRoom
            {
                HotelId = hotelRoomDto.HotelID,
                RoomId = hotelRoomDto.RoomID,
                RoomNumber = hotelRoomDto.RoomNumber,
                Rate = hotelRoomDto.Rate,
                PitFriendly = hotelRoomDto.PetFriendly,
            };

            return hotelRoom;
        }

        /// <summary>
        /// Helper method that takes a hotelRoom  
        /// and converts it into a hotelRoomDto.
        /// </summary>
        /// <param name="hotelRoom">A unique hotelRoom object</param>
        /// <returns>A hotelRoomDto object</returns>
        private HotelRoomDTO ConvertEntityIntoDTO(HotelRoom hotelRoom)
        {

            HotelRoomDTO hotelRoomDto = new HotelRoomDTO
            {
                HotelID = hotelRoom.HotelId,
                RoomID = hotelRoom.RoomId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PitFriendly,
            };

            return hotelRoomDto;
        }


    }
}
