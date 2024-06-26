﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NexusPlanner.Models
{
    [Table("Logins")]
    public class Login
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
