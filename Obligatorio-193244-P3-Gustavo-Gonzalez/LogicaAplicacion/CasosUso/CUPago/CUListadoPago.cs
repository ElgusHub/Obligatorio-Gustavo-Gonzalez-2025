using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.InterfacesCU.ICUPago;
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
    public class CUListadoPago:ICUListadoPago
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUListadoPago(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        IEnumerable<ListadoPagoDTO> ICUListadoPago.Ejecutar()
        {
            IEnumerable<Pago> pagos = RepoPago.FindAll();
            return MapperPago.ListadoPagoToListadoPagoDTO(pagos);
        }
    }
}
