using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.InterfacesCU.ICUTipoGasto;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTipoGasto
{
    public class CUListadoTipoGasto:ICUListadoTipoGasto
    {
        //Aplico Inyeccion de dependencia
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }

        public CUListadoTipoGasto (IRepositorioTipoGastos repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }

        public IEnumerable<ListadoTipoGastoDTO> Ejecutar()
        {
            IEnumerable<TipoGasto> tipoGastos = RepoTipoGasto.FindAll();
            return MapperTipoGasto.ListTipoGastoToTipoGastoDTO(tipoGastos);
        }
    }
}
