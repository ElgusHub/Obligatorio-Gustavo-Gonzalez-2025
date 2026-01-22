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
    public class CUListarRolParaAltaUsuario : ICUListarRol
    {

        public IRepositorioRol RepoRol { get; set; }
        public CUListarRolParaAltaUsuario(IRepositorioRol repoRol)
        {
            RepoRol = repoRol;
        }

        public IEnumerable<ListarRolParaAltaUsuarioDTO> Ejecutar()
        {
            IEnumerable<Rol> rol = RepoRol.FindAll();
            return MapperRol.ListadoRolToListadoRolDTO (rol);
        }
    }
}
