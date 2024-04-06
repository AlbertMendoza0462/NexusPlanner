using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        public async Task<bool> ExistePorProyectoUsuario(int proyectoId, int usuarioId)
        {
            return await _contexto.Solicitudes
                .AsNoTracking()
                .AnyAsync(entidad => entidad.ProyectoId == proyectoId && entidad.UsuarioId == usuarioId);
        }

        public async Task<Solicitud?> Buscar(int id)
        {
            return await _contexto.Solicitudes
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .AsNoTracking()
                .FirstOrDefaultAsync(entidad => entidad.SolicitudId == id && entidad.Estado != 2 && entidad.Proyecto.Estado != 2);
        }

        public async Task<List<Solicitud>> Listar()
        {
            return await _contexto.Solicitudes
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .Where(entidad => entidad.Estado != 2 && entidad.Proyecto.Estado != 2)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Solicitud>> ListarPorProyecto(int id)
        {
            return await _contexto.Solicitudes
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .Where(entidad => entidad.ProyectoId == id && entidad.Estado != 2 && entidad.Proyecto.Estado != 2)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Solicitud>> ListarPorUsuario(int id)
        {
            return await _contexto.Solicitudes
                .Include(entidad => entidad.Usuario)
                .Include(entidad => entidad.Proyecto)
                .Where(entidad => entidad.UsuarioId == id && entidad.Estado != 2 && entidad.Proyecto.Estado != 2)
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
                throw new Exception("El dato no existe en la base de datos.");
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
                throw new Exception("El dato no existe en la base de datos.");
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

                var guardo = false;

                using (IDbContextTransaction transaction = _contexto.Database.BeginTransaction())
                {

                    try
                    {
                        var colaborador = new ColaboradorProyecto
                        {
                            ProyectoId = entidadDB.ProyectoId,
                            UsuarioId = entidadDB.UsuarioId,
                        };

                        _contexto.Add(colaborador);
                        var guardoColaborador = await _contexto.SaveChangesAsync() > 0;
                        _contexto.Entry(colaborador).State = EntityState.Detached;

                        if (!guardoColaborador)
                        {
                            throw new Exception("No fue posible agregar colaboradores");
                        }

                        _contexto.Entry(entidadDB).State = EntityState.Modified;
                        guardo = await _contexto.SaveChangesAsync() > 0;
                        _contexto.Entry(entidadDB).State = EntityState.Detached;

                        if (!guardo)
                        {
                            throw new Exception("No fue posible aceptar la solicitud");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }



                return guardo;
            }
            else
            {
                throw new Exception("El dato no existe en la base de datos.");
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
