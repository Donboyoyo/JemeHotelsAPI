using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JemeHotelsProject.Migrations
{
    /// <inheritdoc />
    public partial class FinalGuestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Guests",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Guests",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
