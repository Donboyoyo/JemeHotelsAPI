using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JemeHotelsProject.Migrations.JemeHotelsAuthDb
{
    /// <inheritdoc />
    public partial class IAddingAdminandUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "309d3fdf-1064-46fb-96b4-85b071dcd683",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ccb4928-6d6a-43d9-88f3-43ca2f2b8f6c",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "309d3fdf-1064-46fb-96b4-85b071dcd683",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Receptionist", "RECEPTIONIST" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ccb4928-6d6a-43d9-88f3-43ca2f2b8f6c",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Customer", "CUSTOMER" });
        }
    }
}
