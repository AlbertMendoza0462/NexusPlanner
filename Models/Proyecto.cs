using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NexusPlanner.Models
{
    [Table("Proyectos")]
    public class Proyecto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectoId { get; set; }
        [Required, ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre no debe estar en blanco.")]
        public string Nombre { get; set; }
        [AllowNull]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha final no debe estar en blanco.")]
        public DateOnly FechaFinal { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Range(1, int.MaxValue), Required]
        public int Estado { get; set; }
        public Usuario? Usuario { get; set; }
        [NotMapped]
        public List<Usuario>? Colaboradores { get; set; }
        [NotMapped]
        public List<Tarea>? Tareas { get; set; }
    }
}
