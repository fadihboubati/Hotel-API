using Hotel_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    HotelName = "La Mamounia",
                    Country = "Moroco",
                    City = "Marrakech",
                    State = "Marrakech state",
                    PhoneNumber = "XXXXX-XXX-XXXXXX",
                    StreetAddress = "Moroco street"
                },

                new Hotel
                {
                    Id = 2,
                    HotelName = "Al Harir Palace",
                    Country = "Jordan",
                    City = "Amman",
                    State = "Amman state",
                    PhoneNumber = "00962-XXX-XXXXXX",
                    StreetAddress = "Jordan street"
                },

                new Hotel
                {
                    Id = 3,
                    HotelName = "Four Seasons",
                    Country = "Syria",
                    City = "Damascus",
                    State = "Damascus state",
                    PhoneNumber = "00963-XXX-XXXXXX",
                    StreetAddress = "Damascus street"
                }

            );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    RoomName = "A1-1",
                    Layout = Layout.OneBedRoom
                },
                new Room
                {
                    Id = 2,
                    RoomName = "A1-2",
                    Layout = Layout.TwoBedRoom
                },
                new Room
                {
                    Id = 3,
                    RoomName = "A1-3",
                    Layout = Layout.Studio
                }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    AmenityName = "Computer and Internet access"
                },
                new Amenity
                {
                    Id = 2,
                    AmenityName = "Washer and Dryer"
                },
                new Amenity
                {
                    Id = 3,
                    AmenityName = "Towels"
                }

            );

            modelBuilder.Entity<RoomAmenity>().HasKey(
                roomAmenity => new { roomAmenity.RoomId, roomAmenity.AmenityId }
                );

        }
    }
}
