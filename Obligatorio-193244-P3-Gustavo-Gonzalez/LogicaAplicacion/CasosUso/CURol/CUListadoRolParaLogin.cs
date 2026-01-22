using CasosDeUso.DTOs.RolDTO;
using CasosDeUso.InterfacesCU.ICURol;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CURol
{
    public class CUListadoRolParaLogin : ICUListadoRolParaLogin
    {
        public IRepositorioRol RepoRol {  get; set; }

        public CUListadoRolParaLogin(IRepositorioRol reporol)
        {
            RepoRol = reporol;
        }
        public IEnumerable<ListadoRolParaLoginDTO> Ejecutar()
        {
            IEnumerable<Rol> rol = RepoRol.FindAll();
            return MapperRol.ListadoRolLoginToListadoRolLoginDTO(rol);
        }
    }
}
