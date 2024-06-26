﻿using System;
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
    public class TareasController : ControllerBase
    {
        private readonly TareaBLL _bll;

        public TareasController(TareaBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
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

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> Get(int id)
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

        // GET: api/Tareas/5
        [HttpGet("PorUsuario/{id}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> ListarPorUsuario(int id)
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
                return Ok(await _bll.ListarPorUsuario(id));
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET: api/Tareas/5
        [HttpGet("PorProyecto/{id}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> ListarPorProyecto(int id)
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
                return Ok(await _bll.ListarPorProyecto(id));
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET: api/Tareas/5
        [HttpGet("PorProyectoUsuario/{proyectoId}/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> ListarPorProyectoUsuario(int proyectoId, int usuarioId)
        {
            try
            {
                if (proyectoId < 1)
                {
                    throw new Exception($"El campo '{nameof(proyectoId)}' debe ser mayor que 0.");
                }
                else if (usuarioId < 1)
                {
                    throw new Exception($"El campo '{nameof(usuarioId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                return Ok(await _bll.ListarPorProyectoUsuario(proyectoId, usuarioId));
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> Guardar(Tarea entidad)
        {
            try
            {
                if (entidad.TareaId < 0)
                {
                    throw new Exception($"El campo '{nameof(entidad.TareaId)}' no debe ser negativo.");
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
                    throw new Exception($"La '{nameof(entidad.TareaId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Iniciar/{id}")]
        public async Task<ActionResult<bool>> Iniciar(int id)
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
                    var guardo = await _bll.Iniciar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.TareaId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Solicitudes/5
        [HttpPost("Pausar/{id}")]
        public async Task<ActionResult<bool>> Pausar(int id)
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
                    var guardo = await _bll.Pausar(entidad);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
                }
                else
                {
                    throw new Exception($"La '{nameof(entidad.TareaId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
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
                    throw new Exception($"La '{nameof(entidad.TareaId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }

        // DELETE: api/Tareas/5
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
                    var guardo = await _bll.Eliminar(entidad.TareaId);
                    return (guardo) ? Ok(guardo) : Problem("No se pudo eliminar.");
                }
                else
                {
                    throw new Exception($"El '{nameof(entidad.TareaId)}' '{id}' no existe ne la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }
        }
    }
}
