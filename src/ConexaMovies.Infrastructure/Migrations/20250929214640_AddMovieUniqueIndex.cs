using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaMovies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_EpisodeId",
                table: "Movies",
                column: "EpisodeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_EpisodeId",
                table: "Movies");
        }
    }
}
