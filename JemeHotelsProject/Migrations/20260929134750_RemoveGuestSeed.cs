using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGuestSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "39d9d260-25e3-4e33-930b-eb18547dc1b3");

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "bedc78e9-661d-469c-b504-cecdfd9f8a4c");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestID", "Name", "PhoneNo" },
                values: new object[,]
                {
                    { "39d9d260-25e3-4e33-930b-eb18547dc1b3", "Ossai", "08079885512" },
                    { "bedc78e9-661d-469c-b504-cecdfd9f8a4c", "Dr. Obasi", "08056245835" }
                });
        }
    }
}
