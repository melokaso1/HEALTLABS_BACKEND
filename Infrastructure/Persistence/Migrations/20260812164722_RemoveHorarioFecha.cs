using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHorarioFecha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_horario_MedicoId_Fecha",
                table: "horario");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "horario");

            migrationBuilder.CreateIndex(
                name: "IX_horario_MedicoId",
                table: "horario",
                column: "MedicoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_horario_MedicoId",
                table: "horario");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Fecha",
                table: "horario",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_horario_MedicoId_Fecha",
                table: "horario",
                columns: new[] { "MedicoId", "Fecha" },
                unique: true);
        }
    }
}
