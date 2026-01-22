using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.PagoDTO
{
    public class ListaFiltroPagoXFecha
    {
        [DisplayName("Identificador")]
        public int Id { get; set; }

        
        [DisplayName("Método de Pago")]
        public string NombreMetodoPago { get; set; }


        [DisplayName("Tipo de Pago")]
        public string NombreTipoGasto { get; set; }


        [DisplayName("Descripción")]
        public string NombreGasto { get; set; }

        [DisplayName("Fecha de compra")]
        public DateTime FechaCompra { get; set; }

        [DisplayName("Valor de compra")]
        public decimal ValorCompra { get; set; }

        [DisplayName("Saldo pendiente")]
        public decimal SaldoPendiente { get; set; }
    }
}
