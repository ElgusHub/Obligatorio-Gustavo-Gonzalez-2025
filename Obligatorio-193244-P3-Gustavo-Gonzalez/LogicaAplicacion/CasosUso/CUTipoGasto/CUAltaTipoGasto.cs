using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.InterfacesCU.CUTipoGasto;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUAltaTipoGasto : ICUAltaTipoGasto
    {
        public IRepositorioTipoGastos RepoTipoGastos { get; set; }

        public CUAltaTipoGasto(IRepositorioTipoGastos repositorioTipoGastos)
        {
            RepoTipoGastos = repositorioTipoGastos;
        }

        public void Ejecutar(TipoGastoDTO tipoGastoDTO)
        {
            if (tipoGastoDTO == null)
            {
                throw new GastoException("El Tipo de Gasto no puede ser vacío");
            }
            TipoGasto tipoGasto = MapperTipoGasto.TipoGastoDTOToTipoGasto(tipoGastoDTO);
            RepoTipoGastos.Add(tipoGasto);
        }
    }
}
