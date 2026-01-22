using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.DTOs.PagoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUPago
{
    public interface ICUListaEquiposConPagosUnicosMayorA
    {
        IEnumerable<EquiposPorPagosDTO> Ejecutar(decimal monto);
    }
}
