using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NexusPlanner.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFinal = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.ProyectoId);
                    table.ForeignKey(
                        name: "FK_Proyectos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColaboradoresProyecto",
                columns: table => new
                {
                    ColaboradorProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaboradoresProyecto", x => x.ColaboradorProyectoId);
                    table.ForeignKey(
                        name: "FK_ColaboradoresProyecto_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoId");
                    table.ForeignKey(
                        name: "FK_ColaboradoresProyecto_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    SolicitudId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.SolicitudId);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoId");
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    TareaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFinal = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.TareaId);
                    table.ForeignKey(
                        name: "FK_Tareas_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoId");
                    table.ForeignKey(
                        name: "FK_Tareas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellido", "Clave", "Correo", "Estado", "FechaCreacion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "a@a.a", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(3865), "Albert", "8494736796" },
                    { 2, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "i@i.i", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(3898), "Iris", "8494736796" },
                    { 3, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "r@r.r", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(3917), "Ronald", "8494736796" },
                    { 4, "Lopez", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "o@o.o", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(3937), "Oly", "8494736796" },
                    { 5, "Nicole", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "j@j.j", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(3978), "Jarissa", "8494736796" }
                });

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "ProyectoId", "Descripcion", "Estado", "FechaCreacion", "FechaFinal", "Nombre", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4101), new DateOnly(2025, 3, 3), "Proyecto de ejemplo", 1 },
                    { 2, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4103), new DateOnly(2026, 6, 6), "Proyecto de ejemplo 1", 2 },
                    { 3, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4104), new DateOnly(2027, 9, 9), "Proyecto de ejemplo 2", 3 }
                });

            migrationBuilder.InsertData(
                table: "ColaboradoresProyecto",
                columns: new[] { "ColaboradorProyectoId", "ProyectoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 1, 3 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 4 },
                    { 6, 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Solicitudes",
                columns: new[] { "SolicitudId", "Estado", "FechaCreacion", "FechaRespuesta", "ProyectoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4188), null, 1, 4 },
                    { 2, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4189), null, 1, 5 },
                    { 3, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4190), null, 2, 1 },
                    { 4, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4191), null, 2, 5 },
                    { 5, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4192), null, 3, 1 },
                    { 6, 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4193), null, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "TareaId", "Descripcion", "Estado", "FechaCreacion", "FechaFinal", "Nombre", "ProyectoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4151), new DateOnly(2024, 9, 10), "Tarea de ejemplo", 1, 2 },
                    { 2, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4153), new DateOnly(2024, 9, 10), "Tarea de ejemplo 1", 1, 2 },
                    { 3, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4155), new DateOnly(2024, 9, 10), "Tarea de ejemplo 2", 1, 3 },
                    { 4, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4156), new DateOnly(2024, 9, 10), "Tarea de ejemplo 3", 1, 3 },
                    { 5, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4157), new DateOnly(2024, 9, 10), "Tarea de ejemplo 4", 2, 3 },
                    { 6, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4159), new DateOnly(2024, 9, 10), "Tarea de ejemplo 5", 2, 3 },
                    { 7, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4160), new DateOnly(2024, 9, 10), "Tarea de ejemplo 6", 2, 4 },
                    { 8, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4161), new DateOnly(2024, 9, 10), "Tarea de ejemplo 7", 2, 4 },
                    { 9, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4162), new DateOnly(2024, 9, 10), "Tarea de ejemplo 8", 3, 4 },
                    { 10, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4164), new DateOnly(2024, 9, 10), "Tarea de ejemplo 9", 3, 4 },
                    { 11, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4165), new DateOnly(2024, 9, 10), "Tarea de ejemplo 10", 3, 5 },
                    { 12, "Esta es la descripción", 1, new DateTime(2024, 3, 14, 11, 36, 44, 545, DateTimeKind.Local).AddTicks(4167), new DateOnly(2024, 9, 10), "Tarea de ejemplo 11", 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColaboradoresProyecto_ProyectoId",
                table: "ColaboradoresProyecto",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ColaboradoresProyecto_UsuarioId",
                table: "ColaboradoresProyecto",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ProyectoId",
                table: "Solicitudes",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_UsuarioId",
                table: "Solicitudes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ProyectoId",
                table: "Tareas",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColaboradoresProyecto");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
