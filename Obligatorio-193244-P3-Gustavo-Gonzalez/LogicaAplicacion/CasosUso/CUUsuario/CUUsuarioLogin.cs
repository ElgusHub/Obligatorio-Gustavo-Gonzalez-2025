using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.ICUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUUsuarioLogin: ICUUsuarioLogin
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUUsuarioLogin(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public UsuarioLogueadoDTO Ejecutar(UsuarioLoginDTO usuarioLoginDTO)
        {
            Usuario usuario = RepoUsuario.UsuarioPorMailYPass(usuarioLoginDTO.Mail, usuarioLoginDTO.Password);
            if (usuario == null)
            {
                throw new UsuarioException("Usuario o Contraseña incorrecto");
            }
            return MapperUsuario.UsuarioLoginDTOToUsuarioLogin(usuario);
            
        }

       
    }
}
