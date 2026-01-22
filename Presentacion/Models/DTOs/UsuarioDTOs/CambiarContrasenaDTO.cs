using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuarioDTOs
{
    public class CambiarContrasenaDTO
    {
        public int UsuarioId { get; set; }
        public string Mail { get; set; }
    }
}
