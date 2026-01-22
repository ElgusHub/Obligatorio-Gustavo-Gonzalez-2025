using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.DTOs.UsuarioDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.AuditoriaDTO
{
    public class AuditoriaDTO
    {
        public string Accion { get; set; }
        [DisplayName("Fecha de realizado")]
        public DateTime Fecha { get; set; }
        [DisplayName("Id de Usuario")]
        public int UsuarioId { get; set; }
        [DisplayName("Nombre de Usuario")]
        public string NombreUsuario { get; set; }
        [DisplayName("Rol de Usuario")]
        public string Rol { get; set; }

        [DisplayName("Id Tipo Gasto")]
        public int TipoGastoId { get; set; }
        [DisplayName("Nombre de Tipo de Gasto")]
        public string TipoGastoNombre { get; set; }
        [DisplayName("Descripcion de Tipo de Gasto")]
        public string DescripcionTipoGasto { get; set; }
        public int Activo { get; set; }
    }
}
