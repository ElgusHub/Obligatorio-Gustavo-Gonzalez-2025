using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagoDTO
{
    public class BuscarPagoPorIdDTO
    {
        [DisplayName("Identificador")]
        public int IdPago { get; set; }


        [DisplayName("Método de pago")]
        public string MetodoPago { get; set; }


        [DisplayName("Tipo de pago")]
        public string TipoPago { get; set; }


        [DisplayName("Id de usuario")]
        public int UsuarioId { get; set; }


        [DisplayName("Nombre usuario")]
        public string NombreUsuario { get; set; }


        [DisplayName("Descripción de pago")]
        public string DescripcionPago { get; set; }


        [DisplayName("Monto de pago")]
        public decimal? MontoPago { get; set; }
    }
}
