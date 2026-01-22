using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.TipoGastoDTOs
{
    public class TipoGastoDTO
    {
        
        [Required(ErrorMessage ="El Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; }
    }
}
