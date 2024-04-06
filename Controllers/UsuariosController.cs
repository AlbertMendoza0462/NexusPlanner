using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusPlanner.BLL;
using NexusPlanner.Correos;
using NexusPlanner.DAL;
using NexusPlanner.Models;
using NexusPlanner.Utils;
using NexusPlanner.ViewModels;

namespace NexusPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioBLL _bll;

        public UsuariosController(UsuarioBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosViewModel>>> GetAll()
        {
            try
            {
                var usuarios = await _bll.Listar();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosViewModel>> Get(int id)
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
                var entidad = await _bll.BuscarViewModel(id);
                return (entidad == null) ? NotFound() : Ok(entidad);
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("PorNombreApellidoCorreo/{texto}")]
        public async Task<ActionResult<List<UsuariosViewModel>>> ListarPorNombreApellidoCorreo(string texto)
        {
            try
            {
                if (texto.Length < 1)
                {
                    throw new Exception($"El campo '{nameof(texto)}' debe tener al menos un caractér.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                return Ok(await _bll.ListarPorNombreApellidoCorreo(texto));
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> Guardar(NuevoUsuarioViewModel entidad)
        {
            try
            {
                if (entidad.UsuarioId < 0)
                {
                    throw new Exception($"El campo '{nameof(entidad.UsuarioId)}' no debe ser negativo.");
                }
                if (entidad.Nombre.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.Nombre)}' no debe estar en blanco.");
                }
                if (entidad.Apellido.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.Apellido)}' no debe estar en blanco.");
                }
                if (entidad.UsuarioId == 0)
                {
                    if (entidad.Correo.IsNullOrEmpty())
                    {
                        throw new Exception($"El campo '{nameof(entidad.Correo)}' no debe estar en blanco.");
                    }
                    if (entidad.Clave.IsNullOrEmpty())
                    {
                        throw new Exception($"El campo '{nameof(entidad.Clave)}' no debe estar en blanco.");
                    }
                    if (await _bll.ExistePorCorreo(entidad.Correo))
                    {
                        throw new Exception("Este correo no está disponible.");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.Guardar(entidad);
                if (guardo)
                {
                    try
                    {
                        CorreoBienvenida plantilla = new CorreoBienvenida(entidad.Nombre);
                        Correo correo = new Correo(entidad.Correo, entidad.Nombre, plantilla.Asunto, plantilla.Cuerpo);
                        correo.Send();
                    }
                    catch (Exception e)
                    {

                    }
                    return Ok(guardo);
                }
                else
                {
                    return Problem("No se pudo guardar.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Login")]
        public async Task<ActionResult<UsuariosViewModel>> Login(NuevoUsuarioViewModel entidad)
        {
            try
            {
                if (entidad.Correo.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.Correo)}' no debe estar en blanco.");
                }
                if (entidad.Clave.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.Clave)}' no debe estar en blanco.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var usuario = await _bll.BuscarPorCorreoYClave(entidad.Correo, entidad.Clave);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Correo y/o clave incorrectos.");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Logout")]
        public async Task<ActionResult<bool>> Logout(NuevoUsuarioViewModel entidad)
        {
            try
            {
                if (entidad.UsuarioId < 1)
                {
                    throw new Exception($"El campo '{nameof(entidad.UsuarioId)}' debe ser mayor que 0.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                // TODO metodo para cerrar sesion
                var existe = await _bll.Existe(entidad.UsuarioId);
                return (existe) ? Ok(existe) : NotFound("El usuario no existe en la base de datos.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }

        [HttpPost("CambiarClave")]
        public async Task<ActionResult<bool>> CambiarClave(NuevoUsuarioViewModel entidad)
        {
            try
            {
                if (entidad.UsuarioId < 1)
                {
                    throw new Exception($"El campo '{nameof(entidad.UsuarioId)}' debe ser mayor que 0.");
                }
                if (entidad.Clave.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.Clave)}' no debe estar en blanco.");
                }
                if (entidad.ClaveNueva.IsNullOrEmpty())
                {
                    throw new Exception($"El campo '{nameof(entidad.ClaveNueva)}' no debe estar en blanco.");
                }
                if (entidad.ClaveNueva != entidad.ClaveConfirmacion)
                {
                    throw new Exception($"Los campos '{nameof(entidad.Clave)}' y '{nameof(entidad.ClaveConfirmacion)}' no deben ser diferentes.");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Source + ": " + e.Message);
            }

            try
            {
                var guardo = await _bll.CambiarClave(entidad);
                return (guardo) ? Ok(guardo) : Problem("No se pudo guardar.");
            }
            catch (Exception e)
            {
                return Problem(e.Source + ": " + e.Message);
            }
        }
    }
}
