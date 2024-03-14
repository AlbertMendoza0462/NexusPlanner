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
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<ColaboradorProyecto> ColaboradoresProyecto { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId = 1, Nombre = "Albert", Apellido = "Mendoza", Correo = "a@a.a", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1 },
                new Usuario { UsuarioId = 2, Nombre = "Iris", Apellido = "Mendoza", Correo = "i@i.i", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1 },
                new Usuario { UsuarioId = 3, Nombre = "Ronald", Apellido = "Mendoza", Correo = "r@r.r", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1 },
                new Usuario { UsuarioId = 4, Nombre = "Oly", Apellido = "Lopez", Correo = "o@o.o", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1 },
                new Usuario { UsuarioId = 5, Nombre = "Jarissa", Apellido = "Nicole", Correo = "j@j.j", Telefono = "8494736796", Clave = EncryptSHA256.GetSHA256("1234"), FechaCreacion = DateTime.Now, Estado = 1 }
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
        }
    }
}
