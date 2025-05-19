using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgenciaId",
                table: "Envios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DireccionPostal",
                table: "Envios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnvioEficiente",
                table: "Envios",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Envios_AgenciaId",
                table: "Envios",
                column: "AgenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Agencias_AgenciaId",
                table: "Envios",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Agencias_AgenciaId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_AgenciaId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "AgenciaId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "DireccionPostal",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "EnvioEficiente",
                table: "Envios");
        }
    }
}
