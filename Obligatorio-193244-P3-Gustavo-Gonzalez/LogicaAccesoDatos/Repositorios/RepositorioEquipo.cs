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
    public class RepositorioEquipo : IRepositorioEquipo
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioEquipo(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Equipo item)
        {
            item.Validar();
            if (FindByName(item.Nombre) ==null)
            {
                Contexto.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new EquipoException("Ya existe un equipo con ese nombre");
            }
        }

        public void Delete(Equipo item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return Contexto.Equipos.ToList();
        }

        public Equipo FindById(int id)
        {
            return Contexto.Equipos
                .Where(e => e.Id == id)
                .SingleOrDefault();
        }
        public Equipo FindByName(string name)
        {
            return Contexto.Equipos
                .Where(e => e.Nombre == name)
                .SingleOrDefault();
        }

        public void Update(Equipo item)
        {
            throw new NotImplementedException();
        }
    }
}
