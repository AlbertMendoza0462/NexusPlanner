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
                name: "ContadorLogins",
                columns: table => new
                {
                    Mes = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContadorLogins", x => x.Mes);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

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
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<int>(type: "int", nullable: false)
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
                table: "Logins",
                columns: new[] { "LoginId", "FechaCreacion", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 10, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 11, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 14, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 16, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 17, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 18, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 19, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 20, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 21, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 22, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 23, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 24, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 25, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 26, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 27, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 28, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 29, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 30, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 31, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 32, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 33, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 34, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 35, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 36, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 37, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 38, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 39, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 40, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 41, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 42, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 43, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 44, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 45, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 46, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 47, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 48, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 49, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 50, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 51, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 52, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 53, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 54, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 55, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 56, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 57, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 58, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 59, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 60, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 61, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 62, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 63, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 64, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 65, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 66, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 67, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 68, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 69, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 70, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 71, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 72, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 73, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 74, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 75, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 76, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 77, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 78, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 79, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 80, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 81, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 82, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 83, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 84, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 85, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 86, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 87, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 88, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 89, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 90, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 91, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 92, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 93, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 94, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 95, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 96, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 97, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 98, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 99, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 100, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 101, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 102, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 103, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 104, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 105, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 106, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 107, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 108, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 109, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 110, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 111, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 112, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 113, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 114, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 115, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 116, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 117, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 118, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 119, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 120, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 121, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 122, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 123, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 124, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 125, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 126, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 127, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 128, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 129, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 130, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 131, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 132, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 133, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 134, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 135, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 136, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 137, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 138, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 139, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 140, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 141, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 142, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 143, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 144, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 145, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellido", "Clave", "Correo", "Estado", "FechaCreacion", "Nombre", "Rol", "Telefono" },
                values: new object[,]
                {
                    { 1, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "albert@gmail.com", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4392), "Albert", 1, "8494736796" },
                    { 2, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "iris@gmail.com", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4423), "Iris", 1, "8494736796" },
                    { 3, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "ronald@@gmail.com", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4476), "Ronald", 1, "8494736796" },
                    { 4, "Lopez", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "oly@gmail.com", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4500), "Oly", 2, "8494736796" },
                    { 5, "Nicole", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "jarissa@gmail.com", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4523), "Jarissa", 2, "8494736796" }
                });

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "ProyectoId", "Descripcion", "Estado", "FechaCreacion", "FechaFinal", "Nombre", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4648), new DateOnly(2025, 3, 3), "Proyecto de ejemplo", 1 },
                    { 2, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4650), new DateOnly(2026, 6, 6), "Proyecto de ejemplo 1", 2 },
                    { 3, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4652), new DateOnly(2027, 9, 9), "Proyecto de ejemplo 2", 3 }
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
                    { 1, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4772), null, 1, 4 },
                    { 2, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4774), null, 1, 5 },
                    { 3, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4775), null, 2, 1 },
                    { 4, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4776), null, 2, 5 },
                    { 5, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4777), null, 3, 1 },
                    { 6, 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4778), null, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "TareaId", "Descripcion", "Estado", "FechaCreacion", "FechaFinal", "Nombre", "ProyectoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4700), new DateOnly(2024, 9, 10), "Tarea de ejemplo", 1, 2 },
                    { 2, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4702), new DateOnly(2024, 9, 10), "Tarea de ejemplo 1", 1, 2 },
                    { 3, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4704), new DateOnly(2024, 9, 10), "Tarea de ejemplo 2", 1, 3 },
                    { 4, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4706), new DateOnly(2024, 9, 10), "Tarea de ejemplo 3", 1, 3 },
                    { 5, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4707), new DateOnly(2024, 9, 10), "Tarea de ejemplo 4", 2, 3 },
                    { 6, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4709), new DateOnly(2024, 9, 10), "Tarea de ejemplo 5", 2, 3 },
                    { 7, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4710), new DateOnly(2024, 9, 10), "Tarea de ejemplo 6", 2, 4 },
                    { 8, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4712), new DateOnly(2024, 9, 10), "Tarea de ejemplo 7", 2, 4 },
                    { 9, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4713), new DateOnly(2024, 9, 10), "Tarea de ejemplo 8", 3, 4 },
                    { 10, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4717), new DateOnly(2024, 9, 10), "Tarea de ejemplo 9", 3, 4 },
                    { 11, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4718), new DateOnly(2024, 9, 10), "Tarea de ejemplo 10", 3, 5 },
                    { 12, "Esta es la descripción", 1, new DateTime(2024, 4, 8, 18, 50, 21, 330, DateTimeKind.Local).AddTicks(4744), new DateOnly(2024, 9, 10), "Tarea de ejemplo 11", 3, 5 }
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
                name: "ContadorLogins");

            migrationBuilder.DropTable(
                name: "Logins");

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
