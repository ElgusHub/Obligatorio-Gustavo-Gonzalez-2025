using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioMetodoPago : IRepositorioMetodoPago
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioMetodoPago(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(MetodoPago item)
        {
            item.Validar();
            Contexto.Add(item);
            Contexto.SaveChanges();
        }

        public void Delete(MetodoPago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MetodoPago> FindAll()
        {
            return Contexto.MetodoPagos.ToList();
        }

        public MetodoPago FindById(int id)
        {
            return Contexto.MetodoPagos
                .Where(m => m.Id == id)
                .SingleOrDefault();
        }
        public void Update(MetodoPago item)
        {
            throw new NotImplementedException();
        }
    }
}
