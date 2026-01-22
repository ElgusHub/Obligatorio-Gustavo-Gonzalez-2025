using CasosDeUso.DTOs.EquipoDTO;
using CasosDeUso.InterfacesCU.ICUEquipo;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEquipo
{
    public class CUListarEquipo : ICUListarEquipo
    {
        public IRepositorioEquipo RepoEquipo { get; set; }
        public CUListarEquipo(IRepositorioEquipo repo)
        {
            RepoEquipo = repo;
        }
        public IEnumerable<EquipoDTOs> Ejecutar()
        {
            IEnumerable<Equipo> equipos = RepoEquipo.FindAll();
            return MapperEquipo.ListEquipoToListEquipoDTO(equipos);
        }
    }
}
