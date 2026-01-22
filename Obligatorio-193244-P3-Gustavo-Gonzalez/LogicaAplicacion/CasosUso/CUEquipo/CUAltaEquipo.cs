using CasosDeUso.InterfacesCU.ICUEquipo;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEquipo
{
    public class CUAltaEquipo : ICUAltaEquipo
    {
        public IRepositorioEquipo RepoEquipo { get; set; }
        public CUAltaEquipo(IRepositorioEquipo repoEquipo)
        {
            RepoEquipo = repoEquipo;
        }

        public void Ejecutar(string nombre)
        {
            if (nombre == null) 
            {
                throw new ArgumentNullException("El nombre esta vacio");
            }
            Equipo equipo = new Equipo(nombre);
            RepoEquipo.Add(equipo);
        }
    }
}
