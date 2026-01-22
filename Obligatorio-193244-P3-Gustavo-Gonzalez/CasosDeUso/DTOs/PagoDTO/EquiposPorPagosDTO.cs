using System.ComponentModel;

namespace CasosDeUso.DTOs.PagoDTO
{
    public class EquiposPorPagosDTO
    {
        [DisplayName("Id Equipo")]
        public int EquipoId { get; set; }

        [DisplayName("Nombre de Equipo")]
        public string EquipoNombre { get; set; }

    }
}
