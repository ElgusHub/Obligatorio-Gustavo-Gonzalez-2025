using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagoDTO
{
    public class ListadoPagoDTO
    {
        [DisplayName("Identificador")]
        public int IdPago { get; set; }

        
        [DisplayName("Descripción de Pago")]
        public string DescripcionPago { get; set; }

        
        [DisplayName("Nombre Usuario")]
        public string NombreUsuario { get; set; }

        
        [DisplayName("Rol")]
        public string RolUsuario { get; set; }

        
        [DisplayName("Tipo de pago")]
        public string TipoPago { get; set; }

        
        [DisplayName("Descripción de Tipo de Pago")]
        public string DescTipoPago { get; set; }


        [DisplayName("Método de Pago")]
        public string MetodoPago { get; set; }
        public decimal? Montopago { get; set; }

    }
}
