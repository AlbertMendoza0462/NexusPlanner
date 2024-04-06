using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NexusPlanner.DAL;
using NexusPlanner.Interfaces.BLL;
using NexusPlanner.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;

namespace NexusPlanner.BLL
{
    public class ProyectoBLL : IBLL<Proyecto>
    {
        public Contexto _contexto { get; }
        private readonly SolicitudBLL _solicitudBLL;
        private readonly TareaBLL _tareaBLL;

        public ProyectoBLL(Contexto contexto, SolicitudBLL solicitudBLL, TareaBLL tareaBLL)
        {
            _contexto = contexto;
            _solicitudBLL = solicitudBLL;
            _tareaBLL = tareaBLL;
        }

        public async Task<bool> Existe(int id)
        {
            return await _contexto.Proyectos
                .AsNoTracking()
                .AnyAsync(entidad => entidad.ProyectoId == id);
        }

        public async Task<Proyecto?> Buscar(int id)
        {
            var proyecto = await _contexto.Proyectos
                .Where(entidad => entidad.ProyectoId == id && entidad.Estado != 2)
                .Include(entidad => entidad.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (proyecto != null)
            {
                proyecto.Colaboradores = await _contexto.Usuarios
                    .Where(entidad => entidad.Estado != 2 && _contexto.ColaboradoresProyecto
                        .Where(c => c.ProyectoId == id)
                        .Select(c => c.UsuarioId)
                        .ToList()
                        .Contains(entidad.UsuarioId)
                     )
                    .AsNoTracking()
                    .ToListAsync();

                proyecto.Tareas = await _contexto.Tareas
                        .Where(tarea => tarea.ProyectoId == proyecto.ProyectoId && tarea.Estado != 2)
                        .ToListAsync();
            }

            return proyecto;
        }

        public async Task<List<Proyecto>> Listar()
        {
            var proyectos = await _contexto.Proyectos
                .Where(entidad => entidad.Estado != 2)
                .ToListAsync();

            foreach (var proyecto in proyectos)
            {
                proyecto.Tareas = await _contexto.Tareas
                    .Where(tarea => tarea.ProyectoId == proyecto.ProyectoId && tarea.Estado != 2)
                    .AsNoTracking()
                    .ToListAsync();
            }

            return proyectos;

        }
        public async Task<List<Proyecto>> ListarMisProyectos(int id)
        {
            var misProyectos = await _contexto.Proyectos
                .Where(entidad => entidad.UsuarioId == id && entidad.Estado != 2)
                .Include(entidad => entidad.Usuario)
                .AsNoTracking()
                .ToListAsync();

            foreach (var proyecto in misProyectos)
            {
                proyecto.Tareas = await _contexto.Tareas
                    .Where(tarea => tarea.ProyectoId == proyecto.ProyectoId && tarea.Estado != 2)
                    .ToListAsync();
            }

            return misProyectos;
        }

        public async Task<List<Proyecto>> ListarOtrosProyectos(int id)
        {

            var proyectos = await _contexto.Proyectos
                .Where(entidad => entidad.Estado != 2 && _contexto.ColaboradoresProyecto
                    .Where(c => c.UsuarioId == id)
                    .Select(c => c.ProyectoId)
                    .ToList()
                    .Contains(entidad.ProyectoId)
                )
                .AsNoTracking()
                .ToListAsync();

            foreach (var proyecto in proyectos)
            {
                proyecto.Tareas = await _contexto.Tareas
                    .Where(tarea => tarea.ProyectoId == proyecto.ProyectoId && tarea.Estado != 2)
                    .ToListAsync();
            }

            return proyectos;
        }

        public async Task<bool> Insertar(Proyecto entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            entidad.Estado = 1;

            bool guardo = false;

            using (IDbContextTransaction transaction = _contexto.Database.BeginTransaction())
            {
                try
                {
                    _contexto.Add(entidad);
                    guardo = await _contexto.SaveChangesAsync() > 0;
                    _contexto.Entry(entidad).State = EntityState.Detached;

                    if (!guardo)
                    {
                        throw new Exception("No fue posible guardar el proyecto");
                    }

                    foreach (var tarea in entidad.Tareas)
                    {
                        tarea.ProyectoId = entidad.ProyectoId;

                        if (!await _tareaBLL.Guardar(tarea))
                        {
                            throw new Exception("No fue posible crear las tareas");
                        }
                    }

                    foreach (var usuario in entidad.Colaboradores)
                    {
                        var solicitud = new Solicitud
                        {
                            ProyectoId = entidad.ProyectoId,
                            UsuarioId = usuario.UsuarioId
                        };


                        if (!await _solicitudBLL.Guardar(solicitud))
                        {
                            throw new Exception("No fue posible enviar las solicitudes");
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

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

                bool guardo = false;

                using (IDbContextTransaction transaction = _contexto.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var tarea in entidadDB.Tareas)
                        {
                            if (!entidad.Tareas.Any(u => u.TareaId == tarea.TareaId || (u.TareaId == tarea.TareaId && u.Estado == 2)))
                            {
                                if (!await _tareaBLL.Eliminar(tarea.TareaId))
                                {
                                    throw new Exception("No fue posible eliminar las tareas");
                                }
                            }
                        }

                        foreach (var tarea in entidad.Tareas)
                        {
                            if (!await _tareaBLL.Guardar(tarea))
                            {
                                throw new Exception("No fue posible guardar las tareas");
                            }
                        }

                        foreach (var usuario in entidadDB.Colaboradores)
                        {
                            if (!entidad.Colaboradores.Any(u => u.UsuarioId == usuario.UsuarioId))
                            {
                                var colaborador = await _contexto.ColaboradoresProyecto
                                    .Where(u => u.UsuarioId == usuario.UsuarioId && u.ProyectoId == entidadDB.ProyectoId)
                                    .SingleOrDefaultAsync();

                                _contexto.Entry(colaborador).State = EntityState.Deleted;
                                var eliminoColaborador = await _contexto.SaveChangesAsync() > 0;
                                _contexto.Entry(colaborador).State = EntityState.Detached;

                                if (!eliminoColaborador)
                                {
                                    throw new Exception("No fue eliminar colaboradores");
                                }
                            }
                        }
                        foreach (var usuario in entidad.Colaboradores)
                        {
                            if (!entidadDB.Colaboradores.Any(u => u.UsuarioId == usuario.UsuarioId))
                            {
                                if (!await _solicitudBLL.ExistePorProyectoUsuario(entidad.ProyectoId, usuario.UsuarioId))
                                {
                                    var solicitud = new Solicitud
                                    {
                                        ProyectoId = entidadDB.ProyectoId,
                                        UsuarioId = usuario.UsuarioId,
                                        FechaCreacion = usuario.FechaCreacion,
                                        Estado = usuario.Estado
                                    };

                                    _contexto.Add(solicitud);
                                    var guardoSolicitud = await _contexto.SaveChangesAsync() > 0;
                                    _contexto.Entry(solicitud).State = EntityState.Detached;

                                    if (!guardoSolicitud)
                                    {
                                        throw new Exception("No fue posible enviar las solicitudes");
                                    }
                                }
                            }
                        }

                        _contexto.Entry(entidadDB).State = EntityState.Modified;
                        guardo = await _contexto.SaveChangesAsync() > 0;
                        _contexto.Entry(entidadDB).State = EntityState.Detached;

                        if (!guardo)
                        {
                            throw new Exception("No fue eliminar colaboradores");
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Hubo un error en la transacción");
                    }
                }

                return guardo;
            }
            else
            {
                throw new Exception("El dato no existe en la base de datos.");
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
                throw new Exception("El dato no existe en la base de datos.");
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
                throw new Exception("El dato no existe en la base de datos.");
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
                throw new Exception("El dato no existe en la base de datos.");
            }
        }
    }
}
