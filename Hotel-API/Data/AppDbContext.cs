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
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    HotelId = 1,
                    HotelName = "La Mamounia",
                    Country = "Moroco",
                    City = "Marrakech",
                    State = "Marrakech state",
                    PhoneNumber = "XXXXX-XXX-XXXXXX",
                    StreetAddress = "Moroco street"
                },

                new Hotel
                {
                    HotelId = 2,
                    HotelName = "Al Harir Palace",
                    Country = "Jordan",
                    City = "Amman",
                    State = "Amman state",
                    PhoneNumber = "00962-XXX-XXXXXX",
                    StreetAddress = "Jordan street"
                },

                new Hotel
                {
                    HotelId = 3,
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
                    RoomId = 1,
                    RoomName = "A1-1",
                    Layout = Layout.OneBedRoom
                },
                new Room
                {
                    RoomId = 2,
                    RoomName = "A1-2",
                    Layout = Layout.TwoBedRoom
                },
                new Room
                {
                    RoomId = 3,
                    RoomName = "A1-3",
                    Layout = Layout.Studio
                }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    AmenityId = 1,
                    AmenityName = "Computer and Internet access"
                },
                new Amenity
                {
                    AmenityId = 2,
                    AmenityName = "Washer and Dryer"
                },
                new Amenity
                {
                    AmenityId = 3,
                    AmenityName = "Towels"
                }

            );


        }
    }
}
