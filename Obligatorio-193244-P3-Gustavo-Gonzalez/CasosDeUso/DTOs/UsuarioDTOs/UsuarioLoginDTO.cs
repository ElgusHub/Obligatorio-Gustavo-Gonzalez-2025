using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.UsuarioDTOs
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Debe ingresar un mail")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Debe ingresar un password")]
        public string Password { get; set; }

        
    }
}
