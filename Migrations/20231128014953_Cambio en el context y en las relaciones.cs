using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambioenelcontextyenlasrelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadesTransporte",
                principalColumn: "id_unidadTransporte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadesTransporte",
                principalColumn: "id_unidadTransporte",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
