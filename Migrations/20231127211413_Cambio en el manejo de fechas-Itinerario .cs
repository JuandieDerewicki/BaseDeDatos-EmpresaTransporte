using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambioenelmanejodefechasItinerario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_Servicios_id_servicio",
                table: "Servicio_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicio_Usuarios",
                table: "Servicio_Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario_PuntoIntermedios",
                table: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropColumn(
                name: "tipo_usuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "fechaHora_llegada",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "fechaHora_partida",
                table: "Itinerarios");

            migrationBuilder.RenameTable(
                name: "Servicio_Usuarios",
                newName: "Servicios_Usuarios");

            migrationBuilder.RenameTable(
                name: "Itinerario_PuntoIntermedios",
                newName: "Itinerarios_PuntosIntermedios");

            migrationBuilder.RenameIndex(
                name: "IX_Servicio_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios",
                newName: "IX_Servicios_Usuarios_id_puntoIntermedio");

            migrationBuilder.RenameIndex(
                name: "IX_Servicio_Usuarios_dni_usuario",
                table: "Servicios_Usuarios",
                newName: "IX_Servicios_Usuarios_dni_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerarios_PuntosIntermedios",
                newName: "IX_Itinerarios_PuntosIntermedios_id_puntoIntermedio");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerario_PuntoIntermedios_id_itinerario",
                table: "Itinerarios_PuntosIntermedios",
                newName: "IX_Itinerarios_PuntosIntermedios_id_itinerario");

            migrationBuilder.AlterColumn<string>(
                name: "fechaNacimiento",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "fecha_llegada",
                table: "Itinerarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fecha_partida",
                table: "Itinerarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hora_llegada",
                table: "Itinerarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hora_partida",
                table: "Itinerarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "costo_final",
                table: "Servicios_Usuarios",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "hora_salida_PI",
                table: "Itinerarios_PuntosIntermedios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "hora_llegada_PI",
                table: "Itinerarios_PuntosIntermedios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios",
                columns: new[] { "id_servicio", "dni_usuario" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerarios_PuntosIntermedios",
                table: "Itinerarios_PuntosIntermedios",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_PuntosIntermedios_Itinerarios_id_itinerario",
                table: "Itinerarios_PuntosIntermedios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_PuntosIntermedios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerarios_PuntosIntermedios",
                column: "id_puntoIntermedio",
                principalTable: "PuntosIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Servicios_Usuarios",
                column: "id_puntoIntermedio",
                principalTable: "PuntosIntermedios",
                principalColumn: "id_puntoIntermedio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Usuarios_Servicios_id_servicio",
                table: "Servicios_Usuarios",
                column: "id_servicio",
                principalTable: "Servicios",
                principalColumn: "id_servicio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_PuntosIntermedios_Itinerarios_id_itinerario",
                table: "Itinerarios_PuntosIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_PuntosIntermedios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerarios_PuntosIntermedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Servicios_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Usuarios_Servicios_id_servicio",
                table: "Servicios_Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerarios_PuntosIntermedios",
                table: "Itinerarios_PuntosIntermedios");

            migrationBuilder.DropColumn(
                name: "fecha_llegada",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "fecha_partida",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "hora_llegada",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "hora_partida",
                table: "Itinerarios");

            migrationBuilder.RenameTable(
                name: "Servicios_Usuarios",
                newName: "Servicio_Usuarios");

            migrationBuilder.RenameTable(
                name: "Itinerarios_PuntosIntermedios",
                newName: "Itinerario_PuntoIntermedios");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicio_Usuarios",
                newName: "IX_Servicio_Usuarios_id_puntoIntermedio");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios",
                newName: "IX_Servicio_Usuarios_dni_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios",
                newName: "IX_Itinerario_PuntoIntermedios_id_puntoIntermedio");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_PuntosIntermedios_id_itinerario",
                table: "Itinerario_PuntoIntermedios",
                newName: "IX_Itinerario_PuntoIntermedios_id_itinerario");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaNacimiento",
                table: "Usuarios",
                type: "datetime2",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "tipo_usuario",
                table: "Usuarios",
                type: "nvarchar(30)",
                maxLength: 30,
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

            migrationBuilder.AlterColumn<double>(
                name: "costo_final",
                table: "Servicio_Usuarios",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "hora_salida_PI",
                table: "Itinerario_PuntoIntermedios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "hora_llegada_PI",
                table: "Itinerario_PuntoIntermedios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicio_Usuarios",
                table: "Servicio_Usuarios",
                columns: new[] { "id_servicio", "dni_usuario" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario_PuntoIntermedios",
                table: "Itinerario_PuntoIntermedios",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                table: "Itinerario_PuntoIntermedios",
                column: "id_itinerario",
                principalTable: "Itinerarios",
                principalColumn: "id_itinerario",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Servicio_Usuarios_Servicios_id_servicio",
                table: "Servicio_Usuarios",
                column: "id_servicio",
                principalTable: "Servicios",
                principalColumn: "id_servicio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                table: "Servicio_Usuarios",
                column: "dni_usuario",
                principalTable: "Usuarios",
                principalColumn: "dni",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
