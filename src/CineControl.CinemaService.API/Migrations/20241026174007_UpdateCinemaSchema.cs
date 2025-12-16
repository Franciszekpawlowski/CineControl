using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.CinemaService.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCinemaSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Theaters_TheaterId",
                table: "Seats",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");
        }
    }
}
