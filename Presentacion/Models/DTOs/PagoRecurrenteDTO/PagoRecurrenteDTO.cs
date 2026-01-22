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

namespace Web.Models.DTOs.PagoRecurrenteDTO
{
    public class PagoRecurrenteDTO
    {

        [Required(ErrorMessage = "Ingrese una Descripcion")]
        public string? Descripcion { get; set; }


        [DisplayName("Fecha desde")]
        public DateTime FechaDesde { get; set; } = DateTime.Today;


        [DisplayName("Fecha hasta")]
        public DateTime FechaHasta { get; set; } = DateTime.Today;


        [DisplayName("Valor de compra")]
        public decimal ValorCompra { get; set; }


        [DisplayName("Seleccione un Usuario ")]
        public string? UsuarioMail { get; set; }


        [DisplayName("Seleccione un Tipo de Gasto ")]
        public int TipoGastoId { get; set; }


        [DisplayName("Seleccione un Método de Gasto ")]
        public int MetodoPagoId { get; set; }


        public IEnumerable<ListadoUsuarioDTO> Usuarios { get; set; } = new List<ListadoUsuarioDTO>();
        public IEnumerable<ListadoTipoGastoDTO> TipoGastos { get; set; } = new List<ListadoTipoGastoDTO>();
        public IEnumerable<ListadoMetodoPagoDTO> MetodoPagos { get; set; } = new List<ListadoMetodoPagoDTO>();
    }
}
