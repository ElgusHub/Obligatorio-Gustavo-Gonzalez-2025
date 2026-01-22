using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioTipoGastoEF : IRepositorioTipoGastos
    {

        public EmpresaContexto Contexto { get; set; }

        public RepositorioTipoGastoEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(TipoGasto item)
        {
            item.Validar();
            TipoGasto tipo = FindByName(item.Nombre);

            if (tipo == null)
            {
                Contexto.TiposGastos.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new GastoException("Ya existe un tipo de gasto con el mismo nombre");
            }
        }

        public void Delete(TipoGasto item)
        {
            Contexto.TiposGastos.Remove(item);
            Contexto.SaveChanges();
        }

        public IEnumerable<TipoGasto> FindAll()
        {
            return Contexto.TiposGastos;
        }

        public TipoGasto FindById(int id)
        {
            return Contexto.TiposGastos
               .Where(t => t.Id == id)
               .SingleOrDefault();
        }

        public TipoGasto FindByName(string name)
        {
            return Contexto.TiposGastos
                .Where(t=> t.Nombre.ToUpper() == name.ToUpper())
                .SingleOrDefault();
        }
        public void Update(TipoGasto item)
        {
            item.Validar(); 
            Contexto.Update(item);
            Contexto.SaveChanges();
        }
    }
}
