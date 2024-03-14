using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Interfaces.BLL;
using NexusPlanner.Models;

namespace NexusPlanner.BLL
{
    public class ProyectoBLL //: IBLL<Proyecto>
    {
        public Contexto _contexto { get; }

        public ProyectoBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int id)
        {
            return await _contexto.Proyectos
                .AsNoTracking()
                .AnyAsync(entidad => entidad.ProyectoId == id);
        }

        public async Task<Proyecto?> Buscar(int id)
        {
            return await _contexto.Proyectos
                .Include(entidad => entidad.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.ProyectoId == id && entidad.Estado != 2);
        }

        public async Task<List<Proyecto>> Listar()
        {
            return await _contexto.Proyectos
                .Where(entidad => entidad.Estado != 2)
                //.Include(entidad => entidad.Colaboradores)
                .Include(entidad => entidad.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insertar(Proyecto entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.Estado = 1;

            _contexto.Add(entidad);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(entidad).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(Proyecto entidad)
        {
            var entidadDB = await Buscar(entidad.ProyectoId);
            if (entidadDB != null)
            {
                //entidadDB.ProyectoId = entidad.ProyectoId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
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

        public async Task<bool> Terminar(Proyecto entidad)
        {
            var entidadDB = await Buscar(entidad.ProyectoId);
            if (entidadDB != null)
            {
                //entidadDB.ProyectoId = entidad.ProyectoId;
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

        public async Task<bool> Reanudar(Proyecto entidad)
        {
            var entidadDB = await Buscar(entidad.ProyectoId);
            if (entidadDB != null)
            {
                //entidadDB.ProyectoId = entidad.ProyectoId;
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

        public async Task<bool> Guardar(Proyecto entidad)
        {
            if (entidad.ProyectoId == 0)
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
