using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Interfaces.BLL;
using NexusPlanner.Models;

namespace NexusPlanner.BLL
{
    public class SolicitudBLL : ILecturaBLL<Solicitud>
    {
        public Contexto _contexto { get; }

        public SolicitudBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int id)
        {
            return await _contexto.Solicitudes
                .AsNoTracking()
                .AnyAsync(entidad => entidad.SolicitudId == id);
        }

        public async Task<Solicitud?> Buscar(int id)
        {
            return await _contexto.Solicitudes
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.SolicitudId == id && entidad.Estado != 2);
        }

        public async Task<List<Solicitud>> Listar()
        {
            return await _contexto.Solicitudes
                .Where(entidad => entidad.Estado != 2)
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insertar(Solicitud entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.Estado = 1;

            _contexto.Add(entidad);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(entidad).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Cancelar(Solicitud entidad)
        {
            var entidadDB = await Buscar(entidad.SolicitudId);
            if (entidadDB != null)
            {

                //entidadDB.SolicitudId = entidad.SolicitudId;
                //entidadDB.ProyectoId = entidad.ProyectoId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                entidadDB.FechaRespuesta = DateTime.Now;
                //entidadDB.FechaCreacion = entidad.FechaCreacion;
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

        public async Task<bool> Rechazar(Solicitud entidad)
        {
            var entidadDB = await Buscar(entidad.SolicitudId);
            if (entidadDB != null)
            {

                //entidadDB.SolicitudId = entidad.SolicitudId;
                //entidadDB.ProyectoId = entidad.ProyectoId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                entidadDB.FechaRespuesta = DateTime.Now;
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

        public async Task<bool> Aceptar(Solicitud entidad)
        {
            var entidadDB = await Buscar(entidad.SolicitudId);
            if (entidadDB != null)
            {

                //entidadDB.SolicitudId = entidad.SolicitudId;
                //entidadDB.ProyectoId = entidad.ProyectoId;
                //entidadDB.UsuarioId = entidad.UsuarioId;
                entidadDB.FechaRespuesta = DateTime.Now;
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

        public async Task<bool> Guardar(Solicitud entidad)
        {
            if (entidad.SolicitudId == 0)
            {
                return await Insertar(entidad);
            }
            else
            {
                throw new Exception("La solicitud no se puede modificar de esta manera.");
            }
        }
    }
}
