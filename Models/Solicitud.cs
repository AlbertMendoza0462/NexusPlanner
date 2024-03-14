using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NexusPlanner.Models
{
    [Table("Solicitudes")]
    public class Solicitud
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolicitudId { get; set; }
        [Required, ForeignKey(nameof(Proyecto))]
        public int ProyectoId { get; set; }
        [Required, ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        [AllowNull]
        public DateTime? FechaRespuesta { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [MinLength(1), Required]
        public int Estado { get; set; }
        public Proyecto? Proyecto { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
