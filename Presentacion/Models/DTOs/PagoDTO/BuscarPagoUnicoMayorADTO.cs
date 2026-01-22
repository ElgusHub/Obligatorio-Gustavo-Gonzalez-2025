using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.PagoDTO
{
    public class BuscarPagoUnicoMayorADTO
    {
        [Required(ErrorMessage = "Debe ingresar un monto")]

        public decimal? Monto { get; set; }
    }
}
