using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
