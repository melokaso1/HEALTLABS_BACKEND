using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovePersonaDireccionTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_persona_direcciones_tipo",
                table: "persona_direcciones");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "persona_direcciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "persona_direcciones",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_persona_direcciones_tipo",
                table: "persona_direcciones",
                sql: "\"Tipo\" IS NULL OR \"Tipo\" IN ('residencia', 'trabajo')");
        }
    }
}
