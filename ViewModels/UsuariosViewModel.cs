using NexusPlanner.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NexusPlanner.ViewModels
{
    public class UsuariosViewModel
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Correo { get; }
        public string? Telefono { get; set; }
        public int? Estado { get; set; }
        public List<Proyecto>? Proyectos { get; set; }

        public UsuariosViewModel() { }

        public UsuariosViewModel(Usuario usuario)
        {
            this.UsuarioId = usuario.UsuarioId;
            this.Nombre = usuario.Nombre;
            this.Apellido = usuario.Apellido;
            this.Correo = usuario.Correo;
            this.Telefono = usuario.Telefono;
            this.Estado = usuario.Estado;
            this.Proyectos = usuario.Proyectos;
        }

        public static List<UsuariosViewModel> ToUsuariosViewModel(List<Usuario> usuarios)
        {
            var usuariosViewModel = new List<UsuariosViewModel>();
            foreach (var usuario in usuarios)
            {
                var usuarioViewModel = new UsuariosViewModel(usuario);
                usuariosViewModel.Add(usuarioViewModel);
            }
            return usuariosViewModel;
        }
    }

    public class NuevoUsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Clave { get; set; }
        public string? ClaveNueva { get; set; }
        public string? ClaveConfirmacion { get; set; }
    }
}
