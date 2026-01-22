using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.UsuarioDTOs
{
    public class ListadoUsuarioDTO
    {
        [DisplayName("Identificador")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
    }
}
