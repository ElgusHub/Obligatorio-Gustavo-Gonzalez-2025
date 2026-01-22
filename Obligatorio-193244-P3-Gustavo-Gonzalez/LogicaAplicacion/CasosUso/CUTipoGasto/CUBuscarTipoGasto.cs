using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.InterfacesCU.ICUTipoGasto;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTipoGasto
{
    public class CUBuscarTipoGasto:ICUBuscarTipoGasto
    {
        //INYECCION DE DEPENDENCIA
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }

        public CUBuscarTipoGasto(IRepositorioTipoGastos repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }

        //METODO QUE LLAMO DESDE EL CONTROLADOR
        public DetalleTipoGastoDTO Ejecutar(int id)
        {
            TipoGasto tipogasto = RepoTipoGasto.FindById(id);
            if (tipogasto != null)
            {
                return MapperTipoGasto.TipoGastoToDetalleTipoGastoDTO(tipogasto);
            }
            else
            {
                throw new GastoException("No se encontró un Tipo de gasto con ese id");
            }
        }

    }
}
