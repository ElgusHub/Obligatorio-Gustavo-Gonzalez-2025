using CasosDeUso.InterfacesCU.ICUListadoMetodoPago;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoWebAPIController : ControllerBase
    {

        public ICUListadoMetodoPago CUListadoMetodoPago { get; set; }
        public MetodoPagoWebAPIController(ICUListadoMetodoPago cuListadoMetodoPago)
        {
            CUListadoMetodoPago = cuListadoMetodoPago;
        }


        /// <summary>
        /// Listado de Metodo de pago
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("lista-metodo-pago")]
        public IActionResult ListaMetodoPago()
        {
            try
            {
                return Ok(CUListadoMetodoPago.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }

        }


        // GET: api/<MetodoPagoWebAPIController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<MetodoPagoWebAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MetodoPagoWebAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MetodoPagoWebAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MetodoPagoWebAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
