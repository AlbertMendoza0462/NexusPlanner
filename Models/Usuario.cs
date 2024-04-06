using NexusPlanner.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NexusPlanner.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(NuevoUsuarioViewModel usuario)
        {
            UsuarioId = usuario.UsuarioId;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Correo = usuario.Correo;
            Telefono = usuario.Telefono;
            Clave = usuario.Clave;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre no debe estar en blanco.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido no debe estar en blanco.")]
        public string Apellido { get; set; }
        [EmailAddress, Required(ErrorMessage = "El correo debe tener el formato correcto.")]
        public string Correo { get; set; }
        [Phone, AllowNull]
        public string? Telefono { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue), Required]
        public int Estado { get; set; } = 1;
        [Required(ErrorMessage = "La clave no debe estar en blanco.")]
        public string Clave { get; set; }
        [NotMapped, AllowNull]
        public List<Proyecto>? Proyectos { get; set; }
    }
}
