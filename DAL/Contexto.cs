using NexusPlanner.Models;
using Microsoft.EntityFrameworkCore;
using NexusPlanner.Utils;

namespace NexusPlanner.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<ContadorLogin> ContadorLogins { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<ColaboradorProyecto> ColaboradoresProyecto { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Tarea>()
            //    .HasOne(x => x.Proyecto)
            //    .WithMany(x => x.Tareas)
            //    .HasForeignKey(x => x.TareaId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired();

            modelBuilder.Entity<Solicitud>()
                .HasOne(c => c.Proyecto)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ColaboradorProyecto>()
                .HasOne(c => c.Proyecto)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tarea>()
                .HasOne(c => c.Proyecto)
                .WithMany()
                .HasForeignKey(x => x.ProyectoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId = 1, Nombre = "Albert", Apellido = "Mendoza", Correo = "albert@gmail.com", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1, Rol = 1 },
                new Usuario { UsuarioId = 2, Nombre = "Iris", Apellido = "Mendoza", Correo = "iris@gmail.com", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1, Rol = 1 },
                new Usuario { UsuarioId = 3, Nombre = "Ronald", Apellido = "Mendoza", Correo = "ronald@@gmail.com", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1, Rol = 1 },
                new Usuario { UsuarioId = 4, Nombre = "Oly", Apellido = "Lopez", Correo = "oly@gmail.com", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1, Rol = 2 },
                new Usuario { UsuarioId = 5, Nombre = "Jarissa", Apellido = "Nicole", Correo = "jarissa@gmail.com", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1, Rol = 2 }
                );


            modelBuilder.Entity<Proyecto>().HasData(
                new Proyecto { ProyectoId = 1, UsuarioId = 1, Nombre = "Proyecto de ejemplo", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2025, 3, 3), FechaCreacion = DateTime.Now, Estado = 1 },
                new Proyecto { ProyectoId = 2, UsuarioId = 2, Nombre = "Proyecto de ejemplo 1", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2026, 6, 6), FechaCreacion = DateTime.Now, Estado = 1 },
                new Proyecto { ProyectoId = 3, UsuarioId = 3, Nombre = "Proyecto de ejemplo 2", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2027, 9, 9), FechaCreacion = DateTime.Now, Estado = 1 }
                );

            modelBuilder.Entity<ColaboradorProyecto>().HasData(
                new ColaboradorProyecto { ColaboradorProyectoId = 1, ProyectoId = 1, UsuarioId = 2 },
                new ColaboradorProyecto { ColaboradorProyectoId = 2, ProyectoId = 1, UsuarioId = 3 },
                new ColaboradorProyecto { ColaboradorProyectoId = 3, ProyectoId = 2, UsuarioId = 3 },
                new ColaboradorProyecto { ColaboradorProyectoId = 4, ProyectoId = 2, UsuarioId = 4 },
                new ColaboradorProyecto { ColaboradorProyectoId = 5, ProyectoId = 3, UsuarioId = 4 },
                new ColaboradorProyecto { ColaboradorProyectoId = 6, ProyectoId = 3, UsuarioId = 5 }
                );

            modelBuilder.Entity<Tarea>().HasData(
                new Tarea { TareaId = 1, ProyectoId = 1, UsuarioId = 2, Nombre = "Tarea de ejemplo", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 2, ProyectoId = 1, UsuarioId = 2, Nombre = "Tarea de ejemplo 1", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 3, ProyectoId = 1, UsuarioId = 3, Nombre = "Tarea de ejemplo 2", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 4, ProyectoId = 1, UsuarioId = 3, Nombre = "Tarea de ejemplo 3", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 5, ProyectoId = 2, UsuarioId = 3, Nombre = "Tarea de ejemplo 4", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 6, ProyectoId = 2, UsuarioId = 3, Nombre = "Tarea de ejemplo 5", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 7, ProyectoId = 2, UsuarioId = 4, Nombre = "Tarea de ejemplo 6", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 8, ProyectoId = 2, UsuarioId = 4, Nombre = "Tarea de ejemplo 7", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 9, ProyectoId = 3, UsuarioId = 4, Nombre = "Tarea de ejemplo 8", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 10, ProyectoId = 3, UsuarioId = 4, Nombre = "Tarea de ejemplo 9", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 11, ProyectoId = 3, UsuarioId = 5, Nombre = "Tarea de ejemplo 10", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 },
                new Tarea { TareaId = 12, ProyectoId = 3, UsuarioId = 5, Nombre = "Tarea de ejemplo 11", Descripcion = "Esta es la descripción", FechaFinal = new DateOnly(2024, 9, 10), FechaCreacion = DateTime.Now, Estado = 1 }
                );

            modelBuilder.Entity<Solicitud>().HasData(
                new Solicitud { SolicitudId = 1, ProyectoId = 1, UsuarioId = 4, FechaCreacion = DateTime.Now, Estado = 1 },
                new Solicitud { SolicitudId = 2, ProyectoId = 1, UsuarioId = 5, FechaCreacion = DateTime.Now, Estado = 1 },
                new Solicitud { SolicitudId = 3, ProyectoId = 2, UsuarioId = 1, FechaCreacion = DateTime.Now, Estado = 1 },
                new Solicitud { SolicitudId = 4, ProyectoId = 2, UsuarioId = 5, FechaCreacion = DateTime.Now, Estado = 1 },
                new Solicitud { SolicitudId = 5, ProyectoId = 3, UsuarioId = 1, FechaCreacion = DateTime.Now, Estado = 1 },
                new Solicitud { SolicitudId = 6, ProyectoId = 3, UsuarioId = 2, FechaCreacion = DateTime.Now, Estado = 1 }
                );

            modelBuilder.Entity<Login>().HasData(
                new Login { LoginId = 1, UsuarioId = 1, FechaCreacion = new DateTime(2024, 1, 10) },
                new Login { LoginId = 2, UsuarioId = 1, FechaCreacion = new DateTime(2024, 1, 10) },
                new Login { LoginId = 3, UsuarioId = 1, FechaCreacion = new DateTime(2024, 1, 10) },
                new Login { LoginId = 4, UsuarioId = 1, FechaCreacion = new DateTime(2024, 1, 10) },
                new Login { LoginId = 5, UsuarioId = 1, FechaCreacion = new DateTime(2024, 1, 10) },

                new Login { LoginId = 6, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 7, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 8, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 9, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 10, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 11, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 12, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 13, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 14, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },
                new Login { LoginId = 15, UsuarioId = 2, FechaCreacion = new DateTime(2024, 2, 10) },

                new Login { LoginId = 16, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 17, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 18, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 19, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 20, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 21, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 22, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 23, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 24, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 25, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 26, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 27, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 28, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 29, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },
                new Login { LoginId = 30, UsuarioId = 3, FechaCreacion = new DateTime(2024, 3, 10) },

                new Login { LoginId = 31, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },
                new Login { LoginId = 32, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },
                new Login { LoginId = 33, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },
                new Login { LoginId = 34, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },
                new Login { LoginId = 35, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },
                new Login { LoginId = 36, UsuarioId = 4, FechaCreacion = new DateTime(2024, 4, 5) },

                new Login { LoginId = 37, UsuarioId = 5, FechaCreacion = new DateTime(2023, 5, 10) },
                new Login { LoginId = 38, UsuarioId = 5, FechaCreacion = new DateTime(2023, 5, 10) },
                new Login { LoginId = 39, UsuarioId = 5, FechaCreacion = new DateTime(2023, 5, 10) },
                new Login { LoginId = 40, UsuarioId = 5, FechaCreacion = new DateTime(2023, 5, 10) },

                new Login { LoginId = 41, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },
                new Login { LoginId = 42, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },
                new Login { LoginId = 43, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },
                new Login { LoginId = 44, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },
                new Login { LoginId = 45, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },
                new Login { LoginId = 46, UsuarioId = 5, FechaCreacion = new DateTime(2023, 6, 10) },

                new Login { LoginId = 47, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 48, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 49, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 50, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 51, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 52, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 53, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 54, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 55, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 56, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 57, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },
                new Login { LoginId = 58, UsuarioId = 5, FechaCreacion = new DateTime(2023, 7, 10) },

                new Login { LoginId = 59, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 60, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 61, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 62, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 63, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 64, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 65, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 66, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 67, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 68, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 69, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 70, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 71, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 72, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 73, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 74, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 75, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 76, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 77, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 78, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 79, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 80, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 81, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },
                new Login { LoginId = 82, UsuarioId = 5, FechaCreacion = new DateTime(2023, 8, 10) },

                new Login { LoginId = 83, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 84, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 85, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 86, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 87, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 88, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 89, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 90, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 91, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 92, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 93, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 94, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 95, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 96, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 97, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 98, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 99, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 100, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 101, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 102, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 103, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 104, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 105, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },
                new Login { LoginId = 106, UsuarioId = 5, FechaCreacion = new DateTime(2023, 9, 10) },

                new Login { LoginId = 107, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 108, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 109, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 110, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 111, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 112, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 113, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 114, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },
                new Login { LoginId = 115, UsuarioId = 5, FechaCreacion = new DateTime(2023, 10, 10) },

                new Login { LoginId = 116, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 117, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 118, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 119, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 120, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 121, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 122, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 123, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 124, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 125, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 126, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 127, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 128, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 129, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },
                new Login { LoginId = 130, UsuarioId = 5, FechaCreacion = new DateTime(2023, 11, 10) },

                new Login { LoginId = 131, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 132, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 133, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 134, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 135, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 136, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 137, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 138, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 139, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 140, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 141, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 142, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 143, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 144, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) },
                new Login { LoginId = 145, UsuarioId = 5, FechaCreacion = new DateTime(2023, 12, 10) }
                );
        }
    }
}
