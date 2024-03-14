using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPlanner.Models
{
    [Table("ColaboradoresProyecto")]
    public class ColaboradorProyecto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColaboradorProyectoId { get; set; }
        [Required, ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        [Required, ForeignKey(nameof(Proyecto))]
        public int ProyectoId { get; set; }
        public Usuario? Usuario { get; set; }
        public Proyecto? Proyecto { get; set; }
        [NotMapped]
        public List<Proyecto> Proyectos { get; set; }
        [NotMapped]
        public List<Usuario> Colaboradores { get; set; }
    }
}
