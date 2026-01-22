using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuarioDTOs
{
    public class DetalleLoginUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
