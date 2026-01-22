using Web.Models.DTOs.RolDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.DTOs.UsuarioDTOs
{
    public class UsusarioLogueadoDTO
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
        
        //public IEnumerable<ListadoRolParaLoginDTO> Roles { get; set; } = new List<ListadoRolParaLoginDTO>();
    }
}
