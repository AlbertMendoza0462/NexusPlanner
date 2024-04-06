using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Interfaces.BLL;
using NexusPlanner.Models;
using NexusPlanner.Utils;
using NexusPlanner.ViewModels;

namespace NexusPlanner.BLL
{
    public class UsuarioBLL
    {
        public Contexto _contexto { get; }

        public UsuarioBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int id)
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .AnyAsync(entidad => entidad.UsuarioId == id && entidad.Estado != 2);
        }

        public async Task<bool> ExistePorCorreo(string correo)
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .AnyAsync(entidad => entidad.Correo == correo);
        }

        private async Task<Usuario?> Buscar(int id)
        {
            return await _contexto.Usuarios
                //.Include(entidad => entidad.Proyectos)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.UsuarioId == id && entidad.Estado != 2);
        }

        public async Task<UsuariosViewModel?> BuscarViewModel(int id)
        {
            var usuario = await _contexto.Usuarios
                //.Include(entidad => entidad.Proyectos)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.UsuarioId == id && entidad.Estado != 2);

            return (usuario != null) ? new UsuariosViewModel(usuario) : null;
        }

        public async Task<UsuariosViewModel?> BuscarPorCorreoYClave(string correo, string clave)
        {
            var usuario = await _contexto.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => (entidad.Correo == correo && entidad.Clave == EncryptSHA256.GetSHA256(clave)) && entidad.Estado != 2);

            return (usuario != null) ? new UsuariosViewModel(usuario) : null;
        }

        public async Task<List<UsuariosViewModel>> Listar()
        {
            return UsuariosViewModel.ToUsuariosViewModel(await _contexto.Usuarios
                .Where(entidad => entidad.Estado != 2)
                //.Include(entidad => entidad.Proyectos)
                .AsNoTracking()
                .ToListAsync());
        }

        public async Task<List<UsuariosViewModel>> ListarPorNombreApellidoCorreo(string texto)
        {
            return UsuariosViewModel.ToUsuariosViewModel(await _contexto.Usuarios
                .Where(entidad => entidad.Estado != 2 && (entidad.Nombre.StartsWith(texto) || entidad.Apellido.StartsWith(texto) || entidad.Correo.StartsWith(texto)))
                //.Include(entidad => entidad.Proyectos)
                .OrderBy(entidad => entidad.Nombre)
                .ThenBy(entidad => entidad.Correo)
                .ThenBy(entidad => entidad.Apellido)
                .AsNoTracking()
                .ToListAsync());
        }

        public async Task<bool> Insertar(NuevoUsuarioViewModel usuario)
        {
            var entidad = new Usuario(usuario);
            entidad.Clave = EncryptSHA256.GetSHA256(entidad.Clave);
            entidad.FechaCreacion = DateTime.Now;
            entidad.Estado = 1;
            entidad.Rol = 2;

            _contexto.Add(entidad);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(entidad).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(NuevoUsuarioViewModel entidad)
        {
            var entidadDB = await Buscar(entidad.UsuarioId);
            if (entidadDB != null)
            {
                //entidadDB.UsuarioId = entidad.UsuarioId;
                entidadDB.Nombre = entidad.Nombre;
                entidadDB.Apellido = entidad.Apellido;
                //entidadDB.Correo = entidad.Correo;
                //entidadDB.Clave = entidad.Clave;
                entidadDB.Telefono = entidad.Telefono;
                //entidadDB.Estado = entidad.Estado;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        // Es la unica funcion que permite cambiar la clave del usuario
        public async Task<bool> CambiarClave(NuevoUsuarioViewModel entidad)
        {
            var entidadDB = await Buscar(entidad.UsuarioId);

            if (entidadDB != null)
            {
                if (EncryptSHA256.GetSHA256(entidad.Clave) != entidadDB.Clave)
                {
                    throw new Exception("La clave actual es incorrecta.");
                }

                entidadDB.Clave = EncryptSHA256.GetSHA256(entidad.ClaveNueva);

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> Guardar(NuevoUsuarioViewModel entidad)
        {
            if (entidad.UsuarioId == 0)
            {
                return await Insertar(entidad);
            }
            else
            {
                return await Modificar(entidad);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var entidadDB = await Buscar(id);
            if (entidadDB != null)
            {
                entidadDB.Estado = 2;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }
    }
}
