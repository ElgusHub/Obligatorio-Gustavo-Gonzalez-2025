using CasosDeUso.DTOs.EquipoDTO;
using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.RolDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.UsuarioDTOs
{
    public class AltaUsuarioDTO
    {
        public int Id { get;  set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener como minimo 8 carateres")]
        public string Pass { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public int RolId { get; set; }


        [DisplayName("Equipo")]
        [Required(ErrorMessage = "Debe seleccionar un Equipo")]
        public int EquipoId { get; set; }


        public IEnumerable<ListarRolParaAltaUsuarioDTO> RolesDisponibles  = new List<ListarRolParaAltaUsuarioDTO>();
        public IEnumerable<EquipoDTOs> Equipos { get; set; } = new List<EquipoDTOs>();
        
    }
}
