using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.DTOs.PagoRecurrenteDTO;
using CasosDeUso.DTOs.PagoUnicoDTO;
using CasosDeUso.InterfacesCU.ICUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso.CUPago;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoWebAPIController : ControllerBase
    {
        public ICUListadoPago CUListadoPago { get; set; }
        public ICUBuscarPagoPorId CUBuscarPagoPorId { get; set; }
        public ICUListarPagosPorUsuario CUListarPagosPorUsuario { get; set; }
        public ICUListaEquiposConPagosUnicosMayorA CUEquiposConPagosUnicosMayorA { get; set; }
        public ICUAltaPagoRecurrente CUAltaPagoRecurrente { get; set; }
        public ICUAltaPagoUnico CUAltaPagoUnico { get; set; }

        public PagoWebAPIController(ICUListadoPago cuListadoPago, ICUBuscarPagoPorId cuBuscarPagoPorId, ICUListarPagosPorUsuario cuBuscarPagosPorUsuario, ICUListarPagosPorUsuario cuListarPagosPorUsuario,
            ICUListaEquiposConPagosUnicosMayorA cuListaEquiposConPagosUnicosMayorA, ICUAltaPagoRecurrente caAltaPagoRecurrente, ICUAltaPagoUnico cuAltaPagoUnico)
        {
            CUListadoPago = cuListadoPago;
            CUBuscarPagoPorId = cuBuscarPagoPorId;
            CUListarPagosPorUsuario = cuBuscarPagosPorUsuario;
            CUEquiposConPagosUnicosMayorA = cuListaEquiposConPagosUnicosMayorA;
            CUAltaPagoRecurrente = caAltaPagoRecurrente;
            CUAltaPagoUnico = cuAltaPagoUnico;
        }



        /// <summary>
        /// Permite obtener la lista de Pago
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // GET: api/<PagoWebAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(CUListadoPago.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }


        /// <summary>
        /// Alta de Pago Unico
        /// </summary>
        /// <param name="altaPagoUnicoDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("alta-pago-unico")]
        public IActionResult AltaPagoUnico([FromBody] AltaPagoUnicoDTO altaPagoUnicoDTO)
        {
            try
            {
                //Para guardar el mail desde el Token obtenido
                string email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                    ?? User.FindFirst(ClaimTypes.Email)?.Value;
                if (altaPagoUnicoDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                //altaPagoUnicoDTO.UsuarioMail = email;
                return Ok(CUAltaPagoUnico.Ejecutar(altaPagoUnicoDTO));
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
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
        /// Alta de Pago Recurrente
        /// </summary>
        /// <param name="altaPagoRecurrenteDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("alta-pago-recurrente")]
        public IActionResult AltaPagoRecurrente([FromBody] AltaPagoRecurrenteDTO altaPagoRecurrenteDTO)
        {
            try
            {
                //Para guardar el mail desde el Token obtenido
                string email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                    ?? User.FindFirst(ClaimTypes.Email)?.Value;
                if (altaPagoRecurrenteDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                //altaPagoUnicoDTO.UsuarioMail = email;
                return Ok(CUAltaPagoRecurrente.Ejecutar(altaPagoRecurrenteDTO));
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
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
        /// Permite obtener los datos de un Pago por Id del parámetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/<PagoWebAPIController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id no es correcto");
                }
                return Ok(CUBuscarPagoPorId.Ejecutar(id));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PagoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }

        }

        /// <summary>
        /// Obtener los pagos de usuario logueado
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Empleado, Gerente")]
        // GET api/PagoWebAPI/pagos-usuario/5
        [HttpGet("pagos-usuario")]
        public IActionResult ObtenerPagosPorUnUsuario()
        {
            try
            {
                //Para guardar el mail desde el Token obtenido
                string email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                    ?? User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email correcto");
                }
                return Ok(CUListarPagosPorUsuario.Ejecutar(email));
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
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
        /// Equipos con ususarios con pagos unicos mayor A:
        /// </summary>
        /// <param name="monto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Gerente")]
        [HttpGet("equipos-usuarios/{monto:decimal}")]
        public IActionResult EquiposConPagosUnicosMayorA(decimal monto)
        {
            try
            {
                if (monto <= 0)
                {
                    return BadRequest("El monto no puede ser cero");
                }
                return Ok(CUEquiposConPagosUnicosMayorA.Ejecutar(monto));
            }
            catch (PagoException ex)
            {
                return BadRequest(ex.Message);
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


        // POST api/<PagoWebAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PagoWebAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PagoWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
