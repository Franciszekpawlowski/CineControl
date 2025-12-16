using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.SeanceService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPanoramicPosterUrlToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PanoramicPosterUrl",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PanoramicPosterUrl",
                table: "Movies");
        }
    }
}
