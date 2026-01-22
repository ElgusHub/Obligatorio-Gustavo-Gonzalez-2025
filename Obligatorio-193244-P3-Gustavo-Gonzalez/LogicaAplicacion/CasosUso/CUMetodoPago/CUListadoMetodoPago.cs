using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.InterfacesCU.ICUListadoMetodoPago;
using LogicaAccesoDatos.Repositorios;
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
    public class CUListadoMetodoPago : ICUListadoMetodoPago
    {
        public IRepositorioMetodoPago RepoMetodoPago { get; set; }
        public CUListadoMetodoPago(IRepositorioMetodoPago repoMetodoPago)
        {
            RepoMetodoPago = repoMetodoPago;
        }



        public IEnumerable<ListadoMetodoPagoDTO> Ejecutar()
        {
            IEnumerable<MetodoPago> metodoPagos = RepoMetodoPago.FindAll();
            return MapperMetodoPago.ListMetodoPagoToMetodoPagoDTO(metodoPagos);
        }




    }
}
