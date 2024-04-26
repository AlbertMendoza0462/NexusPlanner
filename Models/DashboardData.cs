using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPlanner.Models
{
    [NotMapped]
    public class DashboardData
    {
        public int totalProyectos { get; set; }
        public int proyectosTerminados { get; set; }
        public int proyectosProceso { get; set; }
        public int totalTareas { get; set; }
        public int tareasTerminados { get; set; }
        public int tareasProceso { get; set; }
        public int tareasnoInicianos { get; set; }
        //public EstadoUsuarios EstadoUsuario { get; set; }
        //public RolesUsuarios Roles { get; set; }
        public List<ContadorLogin> Meses { get; set;  }
    }

    public enum EstadoUsuarios
    {
        Activo = 1,
        Baneado = 2
    }

    public enum RolesUsuarios
    {
        Administrador = 1,
        Usuario = 2
    }
}
