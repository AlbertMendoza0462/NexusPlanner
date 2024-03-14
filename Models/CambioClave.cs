using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPlanner.Models
{
    [NotMapped]
    public class CambioClave
    {
        [Required(ErrorMessage = "La clave actual no debe estar en blanco.")]
        public string ClaveActual { get; set; }
        [Required(ErrorMessage = "La clave nueva no debe estar en blanco.")]
        public string ClaveNueva { get; set; }
        [Required(ErrorMessage = "La confirmación de la clave no debe estar en blanco.")]
        public string ConfirmacionClave { get; set; }
    }
}
