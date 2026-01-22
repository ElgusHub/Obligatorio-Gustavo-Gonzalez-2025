using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.InterfacesCU.ICUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using CasosDeUso.DTOs.PagoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUEquiposConPagosUnicosMayorA : ICUListaEquiposConPagosUnicosMayorA
    {
        public IRepositorioPago RepoPago { get; set; }

        public CUEquiposConPagosUnicosMayorA(IRepositorioPago repoPago) 
        {
            RepoPago = repoPago;
        }

        public IEnumerable<EquiposPorPagosDTO> Ejecutar(decimal monto)
        {
            IEnumerable<Equipo> equipos = RepoPago.BuscarPagosUnicosDeEquiposPorValorMayorA(monto);
            
            if(equipos == null || equipos.Count() ==0)
            {
                throw new ArgumentNullException("No existen Equipos con esa paga mayor a " + monto);
            }
            return MapperPago.ListaEquipoMontoMayorAToListaEquipoMontoMayorADTO(equipos);

        }


    }
}
