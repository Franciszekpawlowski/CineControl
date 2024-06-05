using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.FilmService.API.Migrations
{
    /// <inheritdoc />
    public partial class addtenantID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tenantID",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tenantID",
                table: "Films");
        }
    }
}
