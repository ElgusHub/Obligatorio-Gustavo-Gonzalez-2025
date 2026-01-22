using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.InterfacesCU.ICUTipoGasto;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTipoGasto
{
    public class CUEditarTipoGasto:ICUEditarTipoGasto
    {
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }
        public CUEditarTipoGasto(IRepositorioTipoGastos repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }

        public void Ejecutar(DetalleTipoGastoDTO detalleTipoGasto, int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id no es correcto");
            }
            if (detalleTipoGasto == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            if (detalleTipoGasto.Id != id)
            {
                throw new GastoException("El id del dto no es igual al id");
            }
            TipoGasto tipo = RepoTipoGasto.FindById(id);
            if(tipo == null)
            {
                throw new GastoException("No se encontro el tipo de gasto con ese id");
            }
            tipo.Nombre = detalleTipoGasto.Nombre;
            tipo.Descripcion = detalleTipoGasto.Descripcion;
            RepoTipoGasto.Update(tipo);

        }
    }
}


