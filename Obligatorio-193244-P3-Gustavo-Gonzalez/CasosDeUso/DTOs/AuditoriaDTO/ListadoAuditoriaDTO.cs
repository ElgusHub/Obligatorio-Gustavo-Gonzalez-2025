using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.AuditoriaDTO
{
    public class ListadoAuditoriaDTO
    {
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public int TipoGastoId { get; set; }
        public int Activo { get; set; }
        public string NombreTipoGasto { get; set; }
        public string DescripcionTipoGasto { get; set; }

    }
}
