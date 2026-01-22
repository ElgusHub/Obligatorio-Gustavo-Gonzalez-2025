using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioRol : IRepositorioRol
    {
        public EmpresaContexto Contexto { get; set; }

        public RepositorioRol(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Rol item)
        {
            item.Validar();
            Contexto.Roles.Add(item);
            Contexto.SaveChanges();
        }

        public void Delete(Rol item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rol> FindAll()
        {
            return Contexto.Roles.ToList();
        }

        public Rol FindById(int id)
        {
            return Contexto.Roles
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        public void Update(Rol item)
        {
            throw new NotImplementedException();
        }
        public Rol FindByName(string name)
        {
            return Contexto.Roles
                .Where(c => c.Nombre == name)
                .SingleOrDefault();
        }
    }
}
