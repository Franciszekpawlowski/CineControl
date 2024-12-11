using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineControl.SeanceService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dodanie kolumny TenantId do tabeli Seances
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Seances",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            // Usunięcie istniejącej kolumny TenantID z tabeli Movies
            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "Movies");

            // Dodanie nowej kolumny TenantID jako uuid do tabeli Movies
            migrationBuilder.AddColumn<Guid>(
                name: "TenantID",
                table: "Movies",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Usunięcie kolumny TenantId z tabeli Seances
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Seances");

            // Usunięcie nowej kolumny TenantID jako uuid z tabeli Movies
            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "Movies");

            // Przywrócenie oryginalnej kolumny TenantID jako integer w tabeli Movies
            migrationBuilder.AddColumn<int>(
                name: "TenantID",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
