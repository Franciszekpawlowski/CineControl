using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.CinemaService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Theaters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Seats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Cinemas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Theaters");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Cinemas");
        }
    }
}
