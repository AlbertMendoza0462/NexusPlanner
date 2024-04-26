using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPlanner.Models
{
    public class ContadorLogin
    {
        public int Contador{ get; set; }
        [Key]
        public string Mes { get; set; }
    }
}
