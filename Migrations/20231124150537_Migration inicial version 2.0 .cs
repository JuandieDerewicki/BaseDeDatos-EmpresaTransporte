using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migrationinicialversion20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "calidad_servicio",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "fecha_llegada",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "fecha_partida",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "tipo_servicio",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "destino",
                table: "Servicio_Usuarios");

            migrationBuilder.RenameColumn(
                name: "costo",
                table: "Servicio_Usuarios",
                newName: "costo_final");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaNacimiento",
                table: "Usuarios",
                type: "datetime2",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<double>(
                name: "costo_predeterminado",
                table: "Servicios",
                type: "float",
                maxLength: 50,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "id_puntoIntermedio",
                table: "Servicio_Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tipo_atencion",
                table: "Servicio_Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaHora_llegada",
                table: "Itinerarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaHora_partida",
                table: "Itinerarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "hora_llegada_PI",
                table: "Itinerario_PuntoIntermedios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "hora_salida_PI",
                table: "Itinerario_PuntoIntermedios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Usuarios_id_puntoIntermedio",
                table: "Servicio_Usuarios",
                column: "id_puntoIntermedio",
                unique: true,
                filter: "[id_puntoIntermedio] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadTransportes",
                principalColumn: "id_unidadTransporte",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_PuntoIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_Usuarios_id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropColumn(
                name: "costo_predeterminado",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropColumn(
                name: "tipo_atencion",
                table: "Servicio_Usuarios");

            migrationBuilder.DropColumn(
                name: "fechaHora_llegada",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "fechaHora_partida",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "hora_llegada_PI",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropColumn(
                name: "hora_salida_PI",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.RenameColumn(
                name: "costo_final",
                table: "Servicio_Usuarios",
                newName: "costo");

            migrationBuilder.AlterColumn<string>(
                name: "fechaNacimiento",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "calidad_servicio",
                table: "Servicios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fecha_llegada",
                table: "Servicios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fecha_partida",
                table: "Servicios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tipo_servicio",
                table: "Servicios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "destino",
                table: "Servicio_Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios",
                column: "id_puntoIntermedio",
                principalTable: "PuntoIntermedios",
                principalColumn: "id_puntoIntermedio");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Itinerarios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte",
                principalTable: "UnidadTransportes",
                principalColumn: "id_unidadTransporte");
        }
    }
}
