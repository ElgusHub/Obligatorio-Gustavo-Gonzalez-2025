using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.InterfacesCU.ICUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUFiltarPagosPorFecha : ICUFiltrarPagosXFecha
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUFiltarPagosPorFecha(IRepositorioPago repositorioPago)
        {
            RepoPago = repositorioPago;
        }

        public IEnumerable<ListaFiltroPagoXFecha> Ejecutar(DateTime fecha)
        {
            IEnumerable<Pago> pagos = RepoPago.BuscarPagosMixtosPorFecha(fecha);

            if (pagos == null || pagos.Count() == 0)
            {
                throw new PagoException("No se encontraron pagos únicos con esa fecha");
            }
            return MapperPago.PagoUnicoFiltradoToPagoUnicoFiltradoDTO (pagos);
        }
    }
}
