using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_API.Migrations
{
    public partial class AddRoomAmenitiestoAppDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenity_Amenities_AmenityId",
                table: "RoomAmenity");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenity_Rooms_RoomId",
                table: "RoomAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAmenity",
                table: "RoomAmenity");

            migrationBuilder.RenameTable(
                name: "RoomAmenity",
                newName: "RoomAmenities");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAmenity_AmenityId",
                table: "RoomAmenities",
                newName: "IX_RoomAmenities_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities",
                columns: new[] { "RoomId", "AmenityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities");

            migrationBuilder.RenameTable(
                name: "RoomAmenities",
                newName: "RoomAmenity");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenity",
                newName: "IX_RoomAmenity_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAmenity",
                table: "RoomAmenity",
                columns: new[] { "RoomId", "AmenityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenity_Amenities_AmenityId",
                table: "RoomAmenity",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenity_Rooms_RoomId",
                table: "RoomAmenity",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
