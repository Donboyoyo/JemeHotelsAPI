using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGuestIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Delete the seeded data (You already fixed the Guids to strings here!)
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "39d9d260-25e3-4e33-930b-eb18547dc1b3");

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "bedc78e9-661d-469c-b504-cecdfd9f8a4c");

            // 2. DROP the foreign key and primary key constraints BEFORE altering the columns
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            // 3. ALTER the columns (These are your existing AlterColumn statements)
            migrationBuilder.AlterColumn<string>(
                name: "GuestID",
                table: "Guests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // 4. RECREATE the primary key and foreign key constraints AFTER altering the columns
            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "GuestID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "39d9d260-25e3-4e33-930b-eb18547dc1b3");

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestID",
                keyValue: "bedc78e9-661d-469c-b504-cecdfd9f8a4c");

            migrationBuilder.AlterColumn<Guid>(
                name: "GuestID",
                table: "Guests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "GuestId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestID", "Name", "PhoneNo" },
                values: new object[,]
                {
                    { new Guid("39d9d260-25e3-4e33-930b-eb18547dc1b3"), "Ossai", "08079885512" },
                    { new Guid("bedc78e9-661d-469c-b504-cecdfd9f8a4c"), "Dr. Obasi", "08056245835" }
                });
        }
    }
}
