using CasosDeUso.InterfacesCU.ICUAuditoria;
using ExcepcionesPropias.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaWebAPIController : ControllerBase
    {
        public ICUBuscarAuditoriaPorIdTipoGasto CUBuscarAuditoriaPorIdTipoGasto { get; set; }
        public AuditoriaWebAPIController(ICUBuscarAuditoriaPorIdTipoGasto cuBuscarAuditoriaPorIdTipoGasto)
        {
            CUBuscarAuditoriaPorIdTipoGasto = cuBuscarAuditoriaPorIdTipoGasto;
        }




        /// <summary>
        /// Obtener Auditoria por Id de Tipo de Pago
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrador")]
        [HttpGet("auditoria-tipoGasto/{id}")]
        public IActionResult AuditoriaPorTipoDePagoId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id incorrecto");
                }
                return Ok(CUBuscarAuditoriaPorIdTipoGasto.Ejecutar(id));
            }
            catch (AuditoriaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }




        // GET: api/<AuditoriaWebAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET api/<AuditoriaWebAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuditoriaWebAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuditoriaWebAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuditoriaWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
