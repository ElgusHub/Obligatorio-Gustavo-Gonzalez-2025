using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.InterfacesCU.ICUMetodoPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUMetodoPago
{
    public class CUBucarMetodoPago : ICUBucarMetodoPago
    {
        public IRepositorioMetodoPago RepoMetodoPago { get; set; }
        public CUBucarMetodoPago(IRepositorioMetodoPago repoMetodoPago)
        {
            RepoMetodoPago = repoMetodoPago;
        }


        public DetalleMetodoPagoDTO Ejecutar(int id)
        {
            MetodoPago metodoPago = RepoMetodoPago.FindById(id);
            if (metodoPago != null)
            {
                return MapperMetodoPago.MetodoPagoToMetodoPagoDTO(metodoPago);
            }
            else
            {
                throw new MetodoPagoException("No se encontro el metodo de pago");
            }

        }
    }
}
