using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaMovies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1, "$11$Qu8HTq.K5xN3yLrPtuOY4.jy.ws9IbEgQH9MbjT0BV2HlEyO.jX36", (byte)2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
