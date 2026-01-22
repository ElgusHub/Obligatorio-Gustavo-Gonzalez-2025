using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.ICUPago;
using CasosDeUso.InterfacesCU.ICUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Raven.Client.Exceptions;
using System.Security.Claims;
using WebAPI.Token;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioWebAPIController : ControllerBase
    {
        public ICUListadoUsuarios CUListadoUsuario { get; set; }
        public ICUAltaUsuario CUAltaUsuario { get; set; }
        public ICUBuscarUsuarioPorId CUBuscarUsuarioPorId { get; set; }
        public ICUUsuarioLogin CUUsuarioLogin { get; set; }
        public ICUCambiarContrasena CUCambiarContrasena { get; set; }
        
        public UsuarioWebAPIController(ICUUsuarioLogin cuUsuarioLogin, ICUListadoUsuarios cUListadoUsuario, ICUAltaUsuario cUAltaUsuario, ICUBuscarUsuarioPorId cuBiscarUsuarioPorId, ICUCambiarContrasena cuCambiarContrasena)
        {
            CUUsuarioLogin = cuUsuarioLogin;
            CUListadoUsuario = cUListadoUsuario;
            CUAltaUsuario = cUAltaUsuario;
            CUBuscarUsuarioPorId = cuBiscarUsuarioPorId;
            CUCambiarContrasena = cuCambiarContrasena;
            
        }

        


        /// <summary>
        /// Login Usuario
        /// </summary>
        /// <param name="usuarioLoginDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public IActionResult Post([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            try
            {
                if (usuarioLoginDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                UsuarioLogueadoDTO usuarioLogueadoDTO = CUUsuarioLogin.Ejecutar(usuarioLoginDTO);
                if (usuarioLogueadoDTO != null)
                {
                    usuarioLogueadoDTO.Token = ManejadorToken.CrearToken(usuarioLogueadoDTO);
                    return Ok(usuarioLogueadoDTO);
                }
                else
                {
                    return NotFound("Datos incorrectos");
                }
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }





        /// <summary>
        /// Permite obtener la lista de usuarios
        /// </summary>
        /// <returns></returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="Administrador, Gerente")]
        // GET: api/<UsuarioWebAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                    ?? User.FindFirst(ClaimTypes.Email)?.Value;

                return Ok(CUListadoUsuario.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }

        /// <summary>
        /// Permite obtener usuario por Id
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns ></ returns >

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/UsuarioWebAPI/usuarioPorId/5
        [HttpGet("usuarioPorId/{id:int}")]
        public IActionResult UsuarioPorId(int id)
        {
            ListadoUsuarioPorIdDTO usuario = new ListadoUsuarioPorIdDTO();
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id no es correcto");
                }
                usuario = CUBuscarUsuarioPorId.Ejecutar(id);
                return Ok(usuario);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error");
            }
        }



        /// <summary>
        /// Permite dar el alta a un usuario
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // POST api/<UsuarioWebAPIController>
        [HttpPost("Alta")]
        public IActionResult Post([FromBody] AltaUsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                CUAltaUsuario.Ejecutar(usuarioDTO);
                return CreatedAtRoute("FindById", new { Id = usuarioDTO.Id }, usuarioDTO);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error");
            }

        }



        /// <summary>
        /// Cambiar Contraseña de un usuario
        /// </summary>
        /// <param name="cambiarContrasenaDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrador")]
        [HttpPut("Contrasenia")]
        public IActionResult CambiarPass([FromBody]CambiarContrasenaDTO cambiarContrasenaDTO) 
        {
            try
            {
                if (cambiarContrasenaDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                string nuevaContrasena = CUCambiarContrasena.Ejecutar(cambiarContrasenaDTO);
                return Ok(new { nuevaContrasena });

            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "error");
            }
        }






        // PUT api/<UsuarioWebAPIController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsuarioWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
