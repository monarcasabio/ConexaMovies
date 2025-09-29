using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaMovies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$yZLBC.7RSw3wEWvFU1SB0.5EF4OYZ1I.9HX048hNpzkWWLy1lF4h.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$11$Qu8HTq.K5xN3yLrPtuOY4.jy.ws9IbEgQH9MbjT0BV2HlEyO.jX36");
        }
    }
}
