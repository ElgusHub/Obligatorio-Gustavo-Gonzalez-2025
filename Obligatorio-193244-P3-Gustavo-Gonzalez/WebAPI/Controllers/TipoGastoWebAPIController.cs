using CasosDeUso.InterfacesCU.ICUTipoGasto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoGastoWebAPIController : ControllerBase
    {
        public ICUListadoTipoGasto CUListadoTipoGasto { get; set; }
        public TipoGastoWebAPIController(ICUListadoTipoGasto cUListadoTipoGasto)
        {
            CUListadoTipoGasto = cUListadoTipoGasto;
        }


        /// <summary>
        /// Obtener lista de Tipo Gasto
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Administrador, Gerente")]
        [HttpGet("lista-tipo-gasto")]
        public IActionResult Get()
        {
            try
            {
                //Para guardar el mail desde el Token obtenido
                string email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value
                    ?? User.FindFirst(ClaimTypes.Email)?.Value;

                return Ok(CUListadoTipoGasto.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

            // GET: api/<TipoGastoWebAPIController>
        //    [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<TipoGastoWebAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoGastoWebAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoGastoWebAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoGastoWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
