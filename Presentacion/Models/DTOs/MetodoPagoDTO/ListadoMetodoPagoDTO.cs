using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.MetodoPagoDTO
{
    public class ListadoMetodoPagoDTO
    {
        [DisplayName("Identificador")]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
