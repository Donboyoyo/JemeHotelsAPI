using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
