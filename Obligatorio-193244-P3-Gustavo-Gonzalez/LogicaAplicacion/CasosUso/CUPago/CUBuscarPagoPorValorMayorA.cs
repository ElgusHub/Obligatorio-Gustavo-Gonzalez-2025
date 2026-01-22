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
    public class CUBuscarPagoPorValorMayorA : ICUBuscarPagoPorValorMayorA
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUBuscarPagoPorValorMayorA(IRepositorioPago repo)
        {
            RepoPago = repo;
        }
        public IEnumerable<ListaPagoUnicoMayorADTO> Ejecutar(decimal? monto)
        {
            IEnumerable<PagoUnico> pagoUnicos = RepoPago.BuscarPagoUnicoPorMontoMayorA(monto);
            if(pagoUnicos is null || pagoUnicos.Count() == 0)
            {
                throw new PagoException("No existen pagos únicos mayor a: "+ monto);
            }
            return MapperPago.PagoUnicoToPagoUnicoDTO (pagoUnicos);
        }
    }
}
