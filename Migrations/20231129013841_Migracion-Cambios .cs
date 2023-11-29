using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCambios : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios");

            // Eliminar la columna id
            migrationBuilder.DropColumn("id", "Servicios_Usuarios");

            // Agregar la nueva columna id con propiedad IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Servicios_Usuarios",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "venta",
                table: "Servicios_Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadesTransporte",
                principalColumn: "id_unidadTransporte",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios");

            migrationBuilder.DropColumn(
                name: "venta",
                table: "Servicios_Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Servicios_Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
