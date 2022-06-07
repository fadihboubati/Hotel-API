using Hotel_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }

        private int nextId = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            IdentityRole role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString(),
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            IdentityRoleClaim<string>[] roleClaims = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions", // This matches what we did in Startup.cs
                ClaimValue = permission
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, and Identity needs it
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

            modelBuilder.Entity<HotelRoom>().HasKey(
                hotelRoom => new { hotelRoom.HotelId, hotelRoom.RoomNumber });

            SeedRole(modelBuilder, "Administrator", "create", "update", "delete");
            SeedRole(modelBuilder, "Superuser", "create", "update");
            SeedRole(modelBuilder, "User", "create");
            SeedRole(modelBuilder, "Guest");
        }

    }
}
