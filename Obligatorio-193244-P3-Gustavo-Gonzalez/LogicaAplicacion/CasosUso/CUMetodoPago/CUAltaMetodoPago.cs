using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.UsuarioDTOs;
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
    public class CUAltaMetodoPago:ICUAltaMetodoPago
    {
        public IRepositorioMetodoPago RepoMetodoPago { get; set; }

        public CUAltaMetodoPago(IRepositorioMetodoPago repositorioMetodoPago)
        {
            RepoMetodoPago = repositorioMetodoPago;
        }

        public void Ejecutar(MetodoPagoDTO metodoPagoDTO)
        {
            if (metodoPagoDTO == null)
            {
                throw new MetodoPagoException("El método de pago no puede ser vacío");
            }
            MetodoPago metodoPago = MapperMetodoPago.MetodoPagoDTOToMetodoPago(metodoPagoDTO);
            RepoMetodoPago.Add(metodoPago);
        }
    }
}
