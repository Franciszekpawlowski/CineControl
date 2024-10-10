using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.SeanceService.API.Migrations
{
    /// <inheritdoc />
    public partial class LinkSeancesAndMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Seances_MovieId",
                table: "Seances",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seances_Movies_MovieId",
                table: "Seances",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seances_Movies_MovieId",
                table: "Seances");

            migrationBuilder.DropIndex(
                name: "IX_Seances_MovieId",
                table: "Seances");
        }
    }
}
