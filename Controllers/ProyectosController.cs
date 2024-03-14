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
    public class ProyectosController : ControllerBase
    {
        private readonly ProyectoBLL _bll;

        public ProyectosController(ProyectoBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetAll()
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

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> Get(int id)
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
                var entidad = await _bll.Buscar(id);
                return (entidad == null) ? NotFound() : Ok(entidad);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> Guardar(Proyecto entidad)
        {
            try
            {
                if (entidad.ProyectoId < 0)
                {
                    throw new Exception($"El campo '{nameof(entidad.ProyectoId)}' no debe ser negativo.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(entidad);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Terminar/{id}")]
        public async Task<ActionResult<bool>> Terminar(int id)
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
                    var guardo = await _bll.Terminar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.ProyectoId)}' '{entidad.ProyectoId}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Reanudar/{id}")]
        public async Task<ActionResult<bool>> Reanudar(int id)
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
                    var guardo = await _bll.Reanudar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.ProyectoId)}' '{entidad.ProyectoId}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            try
            {
                if (id <= 0)
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
                var entidad = await _bll.Buscar(id);
                if (entidad != null)
                {
                    var guardo = await _bll.Eliminar(entidad.ProyectoId);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El '{nameof(entidad.ProyectoId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
