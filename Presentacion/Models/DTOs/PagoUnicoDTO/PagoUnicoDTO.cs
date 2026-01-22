using Web.Models.DTOs.MetodoPagoDTO;
using Web.Models.DTOs.TipoGastoDTOs;
using Web.Models.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagoUnicoDTO
{
    public class PagoUnicoDTO
    {
        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string? Descripcion { get; set; }


        [Required(ErrorMessage = "Debe ingresar un número de recibo")]
        [DisplayName("Número de recibo")]
        public int NumeroRecibo { get; set; }


        [DisplayName("Fecha de pago")]
        [Required(ErrorMessage = "La fecha de pago es obligatoria")]
        public DateTime FechaPago { get; set; } = DateTime.Today;


        [DisplayName("Ingrese un Monto de pago")]
        //[Required(ErrorMessage = "Debe ingresar un monto de pago")]
        public decimal MontoPago { get; set; }

        //[DisplayName("Seleccione un Usuario: ")]
        public string? UsuarioMail { get; set; }
        [DisplayName("Seleccione un Tipo de gasto: ")]
        public int TipoGastoId { get; set; }
        //[DisplayName("Método de pago: ")]
        //public int MetodoPagoId { get; set; }
        public IEnumerable<ListadoTipoGastoDTO> TipoGastos { get; set; } = new List<ListadoTipoGastoDTO>();
        //public IEnumerable<ListadoMetodoPagoDTO> MetodoPagos { get; set; } = new List<ListadoMetodoPagoDTO>();

    }
}
