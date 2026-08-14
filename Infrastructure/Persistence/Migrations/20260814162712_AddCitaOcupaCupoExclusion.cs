using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCitaOcupaCupoExclusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OcupaCupo",
                table: "citas",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.Sql("""
                CREATE EXTENSION IF NOT EXISTS btree_gist;

                ALTER TABLE citas
                ADD CONSTRAINT "EX_citas_medico_fecha_hora_activa"
                EXCLUDE USING gist (
                    "MedicoId" WITH =,
                    "Fecha" WITH =,
                    tsrange(("Fecha" + "HoraInicio"), ("Fecha" + "HoraFin"), '[)') WITH &&
                )
                WHERE ("OcupaCupo");
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE citas
                DROP CONSTRAINT "EX_citas_medico_fecha_hora_activa";
                """);

            migrationBuilder.DropColumn(
                name: "OcupaCupo",
                table: "citas");
        }
    }
}
