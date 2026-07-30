using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforGuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestID", "Name", "PhoneNo" },
                values: new object[,]
                {
                    { new Guid("39d9d260-25e3-4e33-930b-eb18547dc1b3"), "Ossai", "08079885512" },
                    { new Guid("bedc78e9-661d-469c-b504-cecdfd9f8a4c"), "Dr. Obasi", "08056245835" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: new Guid("39d9d260-25e3-4e33-930b-eb18547dc1b3"));

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: new Guid("bedc78e9-661d-469c-b504-cecdfd9f8a4c"));
        }
    }
}
