using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.ICUUsuario;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUListadoUsuario:ICUListadoUsuarios
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUListadoUsuario(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public IEnumerable<ListadoUsuarioDTO> Ejecutar()
        {
            IEnumerable<Usuario> usuarios = RepoUsuario.FindAll();
            return MapperUsuario.ListUsuarioToListUsusarioDTO(usuarios);
        }

    }
}
