using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPago:IRepositorio<Pago>
    {
        IEnumerable<Pago> BuscarPagosMixtosPorFecha(DateTime fecha);
        bool ExisteTipoGasto(int id);
        IEnumerable<PagoUnico> BuscarPagoUnicoPorMontoMayorA(decimal? valor);
        IEnumerable<Pago> ObtenerPagoPorUsuario(Usuario usuario);
        IEnumerable<Equipo> BuscarPagosUnicosDeEquiposPorValorMayorA(decimal valor);
    }
}
