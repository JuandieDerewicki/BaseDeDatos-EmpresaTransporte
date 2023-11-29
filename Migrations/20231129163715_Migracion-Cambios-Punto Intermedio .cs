using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCambiosPuntoIntermedio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "id_puntoIntermedio",
                table: "Servicios_Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios",
                columns: new[] { "id_servicio", "dni_usuario", "id_puntoIntermedio" });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios",
                column: "id_puntoIntermedio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "id_puntoIntermedio",
                table: "Servicios_Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios_Usuarios",
                table: "Servicios_Usuarios",
                columns: new[] { "id_servicio", "dni_usuario" });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios",
                column: "id_puntoIntermedio",
                unique: true,
                filter: "[id_puntoIntermedio] IS NOT NULL");
        }
    }
}
