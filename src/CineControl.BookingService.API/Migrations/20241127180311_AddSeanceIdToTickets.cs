using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.BookingService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeanceIdToTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeanceId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeanceId_SeatId",
                table: "Tickets",
                columns: new[] { "SeanceId", "SeatId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeanceId_SeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeanceId",
                table: "Tickets");
        }
    }
}
