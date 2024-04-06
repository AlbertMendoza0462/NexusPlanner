using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusPlanner.BLL;
using NexusPlanner.DAL;
using NexusPlanner.Models;

namespace NexusPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly SolicitudBLL _bll;

        public SolicitudesController(SolicitudBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetAll()
        {
            try
            {
                return Ok(await _bll.Listar());
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpGet("PorProyecto/{id}")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> ListarPorProyecto(int id)
        {
            try
            {
                if (id < 1)
                {
                    throw new Exception($"El campo '{nameof(id)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                return await _bll.ListarPorProyecto(id);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpGet("PorUsuario/{id}")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> ListarPorUsuario(int id)
        {
            try
            {
                if (id < 1)
                {
                    throw new Exception($"El campo '{nameof(id)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                return await _bll.ListarPorUsuario(id);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<bool>> Guardar(Solicitud entidad)
        //{
        //    try
        //    {
        //        if (entidad.SolicitudId < 0)
        //        {
        //            throw new Exception($"El campo '{nameof(entidad.SolicitudId)}' no debe ser negativo.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Source + ": " + e.Message);
        //    }

        //    try
        //    {
        //        var guardo = await _bll.Guardar(entidad);
        //        return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
        //    }
        //    catch (Exception e)
        //    {
        //        return Problem(e.Source + ": " + e.Message);
        //    }
        //}

        // DELETE: api/Solicitudes/5
        [HttpPost("Aceptar/{id}")]
        public async Task<ActionResult<bool>> Aceptar(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new Exception($"El campo '{nameof(id)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var entidad = await _bll.Buscar(id);
                if (entidad != null)
                {
                    var guardo = await _bll.Aceptar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.SolicitudId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Rechazar/{id}")]
        public async Task<ActionResult<bool>> Rechazar(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new Exception($"El campo '{nameof(id)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var entidad = await _bll.Buscar(id);
                if (entidad != null)
                {
                    var guardo = await _bll.Rechazar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.SolicitudId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Cancelar/{id}")]
        public async Task<ActionResult<bool>> Cancelar(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new Exception($"El campo '{nameof(id)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var entidad = await _bll.Buscar(id);
                if (entidad != null)
                {
                    var guardo = await _bll.Cancelar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"Sa '{nameof(entidad.SolicitudId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }
    }
}
