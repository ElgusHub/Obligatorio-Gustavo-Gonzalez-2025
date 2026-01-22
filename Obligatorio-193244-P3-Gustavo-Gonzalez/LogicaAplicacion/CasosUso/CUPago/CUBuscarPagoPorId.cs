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
    public class CUBuscarPagoPorId : ICUBuscarPagoPorId
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUBuscarPagoPorId(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }


        public BuscarPagoPorIdDTO Ejecutar(int id)
        {
            Pago pagos = RepoPago.FindById(id);
            if( pagos == null)
            {
                throw new PagoException("No existe el Pago con ese Id");
            }
            return MapperPago.BuscarPagoPorIdToBuscarPagoPorIdDTO( pagos );
        }
    }
}
