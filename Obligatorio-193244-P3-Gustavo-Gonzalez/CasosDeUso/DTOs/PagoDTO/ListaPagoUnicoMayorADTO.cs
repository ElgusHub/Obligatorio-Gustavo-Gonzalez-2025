using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.PagoDTO
{
    public class ListaPagoUnicoMayorADTO
    {
        [DisplayName("Id de Pago")]
        public int IdPgo { get; set; }

        [DisplayName("Descripción de Pago")]
        public string DescripcionPago { get; set; }

        [DisplayName("Usuario")]
        public string NombreUsuario { get; set; }

        [DisplayName("Valor de compra")]
        public decimal? ValorCompra { get; set; }
    }
}
