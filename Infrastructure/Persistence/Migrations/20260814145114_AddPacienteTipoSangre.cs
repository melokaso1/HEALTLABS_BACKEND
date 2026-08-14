using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPacienteTipoSangre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoSangre",
                table: "pacientes",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_pacientes_tipo_sangre",
                table: "pacientes",
                sql: "\"TipoSangre\" IS NULL OR \"TipoSangre\" IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_pacientes_tipo_sangre",
                table: "pacientes");

            migrationBuilder.DropColumn(
                name: "TipoSangre",
                table: "pacientes");
        }
    }
}
