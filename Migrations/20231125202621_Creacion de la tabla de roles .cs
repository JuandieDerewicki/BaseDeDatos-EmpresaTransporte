using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class Creaciondelatabladeroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_PuntoIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadTransportes",
                table: "UnidadTransportes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PuntoIntermedios",
                table: "PuntoIntermedios");

            migrationBuilder.RenameTable(
                name: "UnidadTransportes",
                newName: "UnidadesTransporte");

            migrationBuilder.RenameTable(
                name: "PuntoIntermedios",
                newName: "PuntosIntermedios");

            migrationBuilder.AddColumn<int>(
                name: "id_rol",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadesTransporte",
                table: "UnidadesTransporte",
                column: "id_unidadTransporte");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PuntosIntermedios",
                table: "PuntosIntermedios",
                column: "id_puntoIntermedio");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_rol = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_rol);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios",
                column: "id_puntoIntermedio",
                principalTable: "PuntosIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios",
                column: "id_puntoIntermedio",
                principalTable: "PuntosIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadesTransporte",
                principalColumn: "id_unidadTransporte",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_id_rol",
                table: "Usuarios",
                column: "id_rol",
                principalTable: "Roles",
                principalColumn: "id_rol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_id_rol",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadesTransporte",
                table: "UnidadesTransporte");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PuntosIntermedios",
                table: "PuntosIntermedios");

            migrationBuilder.DropColumn(
                name: "id_rol",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "UnidadesTransporte",
                newName: "UnidadTransportes");

            migrationBuilder.RenameTable(
                name: "PuntosIntermedios",
                newName: "PuntoIntermedios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadTransportes",
                table: "UnidadTransportes",
                column: "id_unidadTransporte");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PuntoIntermedios",
                table: "PuntoIntermedios",
                column: "id_puntoIntermedio");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios",
                column: "id_puntoIntermedio",
                principalTable: "PuntoIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Usuarios_PuntoIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios",
                column: "id_puntoIntermedio",
                principalTable: "PuntoIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadTransportes",
                principalColumn: "id_unidadTransporte",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
