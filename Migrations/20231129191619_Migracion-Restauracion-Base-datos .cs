using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionRestauracionBasedatos : Migration
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
                    ciudad_destino = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    fecha_partida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_llegada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hora_partida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hora_llegada = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerarios", x => x.id_itinerario);
                });

            migrationBuilder.CreateTable(
                name: "PuntosIntermedios",
                columns: table => new
                {
                    id_puntoIntermedio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosIntermedios", x => x.id_puntoIntermedio);
                });

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

            migrationBuilder.CreateTable(
                name: "UnidadesTransporte",
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
                    table.PrimaryKey("PK_UnidadesTransporte", x => x.id_unidadTransporte);
                });

            migrationBuilder.CreateTable(
                name: "Itinerarios_PuntosIntermedios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_itinerario = table.Column<int>(type: "int", nullable: true),
                    id_puntoIntermedio = table.Column<int>(type: "int", nullable: true),
                    hora_llegada_PI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hora_salida_PI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerarios_PuntosIntermedios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Itinerarios_PuntosIntermedios_Itinerarios_id_itinerario",
                        column: x => x.id_itinerario,
                        principalTable: "Itinerarios",
                        principalColumn: "id_itinerario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Itinerarios_PuntosIntermedios_PuntosIntermedios_id_puntoIntermedio",
                        column: x => x.id_puntoIntermedio,
                        principalTable: "PuntosIntermedios",
                        principalColumn: "id_puntoIntermedio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_dni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaNacimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_dni);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    costo_predeterminado = table.Column<double>(type: "float", nullable: false),
                    id_itinerario = table.Column<int>(type: "int", nullable: true),
                    id_unidadTransporte = table.Column<int>(type: "int", nullable: true),
                    disponibilidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.id_servicio);
                    table.ForeignKey(
                        name: "FK_Servicios_Itinerarios_id_itinerario",
                        column: x => x.id_itinerario,
                        principalTable: "Itinerarios",
                        principalColumn: "id_itinerario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_UnidadesTransporte_id_unidadTransporte",
                        column: x => x.id_unidadTransporte,
                        principalTable: "UnidadesTransporte",
                        principalColumn: "id_unidadTransporte",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicios_Usuarios",
                columns: table => new
                {
                    dni_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    id_puntoIntermedio = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_atencion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    venta = table.Column<bool>(type: "bit", nullable: false),
                    costo_final = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios_Usuarios", x => new { x.id_servicio, x.dni_usuario, x.id_puntoIntermedio });
                    table.ForeignKey(
                        name: "FK_Servicios_Usuarios_PuntosIntermedios_id_puntoIntermedio",
                        column: x => x.id_puntoIntermedio,
                        principalTable: "PuntosIntermedios",
                        principalColumn: "id_puntoIntermedio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_Usuarios_Servicios_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "Servicios",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_Usuarios_Usuarios_dni_usuario",
                        column: x => x.dni_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_PuntosIntermedios_id_itinerario",
                table: "Itinerarios_PuntosIntermedios",
                column: "id_itinerario");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_PuntosIntermedios_id_puntoIntermedio",
                table: "Itinerarios_PuntosIntermedios",
                column: "id_puntoIntermedio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_id_itinerario",
                table: "Servicios",
                column: "id_itinerario");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_id_unidadTransporte",
                table: "Servicios",
                column: "id_unidadTransporte");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Usuarios_dni_usuario",
                table: "Servicios_Usuarios",
                column: "dni_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Usuarios_id_puntoIntermedio",
                table: "Servicios_Usuarios",
                column: "id_puntoIntermedio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itinerarios_PuntosIntermedios");

            migrationBuilder.DropTable(
                name: "Servicios_Usuarios");

            migrationBuilder.DropTable(
                name: "PuntosIntermedios");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Itinerarios");

            migrationBuilder.DropTable(
                name: "UnidadesTransporte");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
