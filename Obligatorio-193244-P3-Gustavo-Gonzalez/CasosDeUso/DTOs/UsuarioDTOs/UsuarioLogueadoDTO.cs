using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.UsuarioDTOs
{
    public class UsuarioLogueadoDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Mail { get; set; }
        //public string Password { get; set; }
        public string Rol { get; set; }
    }
}
