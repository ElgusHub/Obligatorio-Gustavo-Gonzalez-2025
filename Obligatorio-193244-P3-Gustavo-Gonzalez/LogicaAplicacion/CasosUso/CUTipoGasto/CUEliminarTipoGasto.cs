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
    public class CUEliminarTipoGasto:ICUEliminarTipoGasto
    {
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }
        public IRepositorioPago RepoPago { get; set; }

        public CUEliminarTipoGasto(IRepositorioTipoGastos repoTipoGastos, IRepositorioPago repoPago)
        {
            RepoTipoGasto = repoTipoGastos;
            RepoPago = repoPago;
        }

        public void Ejecutar(int id)
        {
            if (id > 0)
            {
                TipoGasto tipoGasto = RepoTipoGasto.FindById(id);

                if (tipoGasto != null)
                {
                    //Verifico que el tipo de gasto no esté en uso, si no está, lo eliminio
                    if(!RepoPago.ExisteTipoGasto(id))
                    {
                        RepoTipoGasto.Delete(tipoGasto);
                    }
                    else
                    {
                        throw new GastoException("No se puede eliminar el Tipo de Gasto porque esta en uso");
                    }
                }
                else
                {
                    throw new GastoException("No existe un tipo de gasto con ese ID");
                }
            }
            else
            {
                throw new ArgumentNullException("El id no es correcto");
            }
        }
    }
}
