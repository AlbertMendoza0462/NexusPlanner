using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Interfaces.BLL;
using NexusPlanner.Models;
using System.Threading.Tasks;

namespace NexusPlanner.BLL
{
    public class TareaBLL : IBLL<Tarea>
    {
        public Contexto _contexto { get; }

        public TareaBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int id)
        {
            return await _contexto.Tareas
                .AsNoTracking()
                .AnyAsync(entidad => entidad.TareaId == id);
        }

        public async Task<Tarea?> Buscar(int id)
        {
            return await _contexto.Tareas
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.TareaId == id && entidad.Estado != 2);
        }

        public async Task<List<Tarea>> Listar()
        {
            return await _contexto.Tareas
                .Where(entidad => entidad.Estado != 2)
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insertar(Tarea entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.Estado = 1;

            _contexto.Add(entidad);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(entidad).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(Tarea entidad)
        {
            var entidadDB = await Buscar(entidad.TareaId);
            if (entidadDB != null)
            {
                //entidadDB.TareaId = entidad.TareaId;
                //entidadDB.ProyectoId = entidad.ProyectoId;
                entidadDB.UsuarioId = entidad.UsuarioId;
                entidadDB.Nombre = entidad.Nombre;
                entidadDB.Descripcion = entidad.Descripcion;
                entidadDB.FechaFinal = entidad.FechaFinal;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
                //entidadDB.Estado = entidad.Estado;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El turno no existe en la base de datos.");
            }
        }

        public async Task<bool> Terminar(Tarea entidad)
        {
            var entidadDB = await Buscar(entidad.TareaId);
            if (entidadDB != null)
            {
                //entidadDB.TareaId = entidad.TareaId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                //entidadDB.Nombre = entidad.Nombre;
                //entidadDB.Descripcion = entidad.Descripcion;
                //entidadDB.FechaFinal = entidad.FechaFinal;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
                entidadDB.Estado = 3;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El turno no existe en la base de datos.");
            }
        }

        public async Task<bool> Reanudar(Tarea entidad)
        {
            var entidadDB = await Buscar(entidad.TareaId);
            if (entidadDB != null)
            {
                //entidadDB.TareaId = entidad.TareaId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                //entidadDB.Nombre = entidad.Nombre;
                //entidadDB.Descripcion = entidad.Descripcion;
                //entidadDB.FechaFinal = entidad.FechaFinal;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
                entidadDB.Estado = 1;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El turno no existe en la base de datos.");
            }
        }

        public async Task<bool> Iniciar(Tarea entidad)
        {
            var entidadDB = await Buscar(entidad.TareaId);
            if (entidadDB != null)
            {
                //entidadDB.TareaId = entidad.TareaId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                //entidadDB.Nombre = entidad.Nombre;
                //entidadDB.Descripcion = entidad.Descripcion;
                //entidadDB.FechaFinal = entidad.FechaFinal;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
                entidadDB.Estado = 4;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El turno no existe en la base de datos.");
            }
        }

        public async Task<bool> Pausar(Tarea entidad)
        {
            var entidadDB = await Buscar(entidad.TareaId);
            if (entidadDB != null)
            {
                //entidadDB.TareaId = entidad.TareaId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                //entidadDB.Nombre = entidad.Nombre;
                //entidadDB.Descripcion = entidad.Descripcion;
                //entidadDB.FechaFinal = entidad.FechaFinal;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
                entidadDB.Estado = 5;

                _contexto.Entry(entidadDB).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(entidadDB).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El turno no existe en la base de datos.");
            }
        }

        public async Task<bool> Guardar(Tarea entidad)
        {
            if (entidad.TareaId == 0)
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
                throw new Exception("El turno no existe en la base de datos.");
            }
        }
    }
}
