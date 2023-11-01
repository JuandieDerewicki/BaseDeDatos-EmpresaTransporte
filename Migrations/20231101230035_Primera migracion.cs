using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class Primeramigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itinerarios",
                columns: table => new
                {
                    id_itinerario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ciudad_origen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ciudad_destino = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerarios", x => x.id_itinerario);
                });

            migrationBuilder.CreateTable(
                name: "PuntoIntermedios",
                columns: table => new
                {
                    id_puntoIntermedio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntoIntermedios", x => x.id_puntoIntermedio);
                });

            migrationBuilder.CreateTable(
                name: "UnidadTransportes",
                columns: table => new
                {
                    id_unidadTransporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_unidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    asientos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadTransportes", x => x.id_unidadTransporte);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    dni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaNacimiento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    tipo_usuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.dni);
                });

            migrationBuilder.CreateTable(
                name: "Itinerario_PuntoIntermedios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_itinerario = table.Column<int>(type: "int", nullable: true),
                    id_puntoIntermedio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario_PuntoIntermedios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Itinerario_PuntoIntermedios_Itinerarios_id_itinerario",
                        column: x => x.id_itinerario,
                        principalTable: "Itinerarios",
                        principalColumn: "id_itinerario");
                    table.ForeignKey(
                        name: "FK_Itinerario_PuntoIntermedios_PuntoIntermedios_id_puntoIntermedio",
                        column: x => x.id_puntoIntermedio,
                        principalTable: "PuntoIntermedios",
                        principalColumn: "id_puntoIntermedio");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_partida = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fecha_llegada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    calidad_servicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tipo_servicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    disponibilidad = table.Column<int>(type: "int", nullable: false),
                    id_itinerario = table.Column<int>(type: "int", nullable: true),
                    id_unidadTransporte = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.id_servicio);
                    table.ForeignKey(
                        name: "FK_Servicios_Itinerarios_id_itinerario",
                        column: x => x.id_itinerario,
                        principalTable: "Itinerarios",
                        principalColumn: "id_itinerario");
                    table.ForeignKey(
                        name: "FK_Servicios_UnidadTransportes_id_unidadTransporte",
                        column: x => x.id_unidadTransporte,
                        principalTable: "UnidadTransportes",
                        principalColumn: "id_unidadTransporte");
                });

            migrationBuilder.CreateTable(
                name: "Servicio_Usuarios",
                columns: table => new
                {
                    dni_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    costo = table.Column<double>(type: "float", nullable: false),
                    destino = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio_Usuarios", x => new { x.id_servicio, x.dni_usuario });
                    table.ForeignKey(
                        name: "FK_Servicio_Usuarios_Servicios_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "Servicios",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Usuarios_Usuarios_dni_usuario",
                        column: x => x.dni_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_PuntoIntermedios_id_itinerario",
                table: "Itinerario_PuntoIntermedios",
                column: "id_itinerario");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerario_PuntoIntermedios_id_puntoIntermedio",
                table: "Itinerario_PuntoIntermedios",
                column: "id_puntoIntermedio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Usuarios_dni_usuario",
                table: "Servicio_Usuarios",
                column: "dni_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itinerario_PuntoIntermedios");

            migrationBuilder.DropTable(
                name: "Servicio_Usuarios");

            migrationBuilder.DropTable(
                name: "PuntoIntermedios");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Itinerarios");

            migrationBuilder.DropTable(
                name: "UnidadTransportes");
        }
    }
}
