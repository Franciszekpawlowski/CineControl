using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.SeanceService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTheaterIdToSeance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheaterId",
                table: "Seances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheaterId",
                table: "Seances");
        }
    }
}
