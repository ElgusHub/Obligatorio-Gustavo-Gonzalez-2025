using CasosDeUso.DTOs.TipoGastoDTOs;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioEquipo RepoEquipo { get; set; }
        public IRepositorioRol RepoRol { get; set; }
        public CUAltaUsuario(IRepositorioUsuario repoUsuario, IRepositorioEquipo repoEquipo, IRepositorioRol reporol)
        {
            RepoUsuario = repoUsuario;
            RepoEquipo = repoEquipo;
            RepoRol = reporol;
        }

        private string Dominio = "@empresa.com";

        public AltaUsuarioDTO Ejecutar(AltaUsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                throw new UsuarioException("El usuario no puede ser vacio");
            }
            //nombre y apellido en minúsculas, sin espacios al principio/fin
            string nombre = (usuarioDTO.Nombre ?? "").Trim().ToLower();
            string apellido = (usuarioDTO.Apellido ?? "").Trim().ToLower();

            //Tomo las 3 primeras letras de cada uno
            string nom3 = nombre.Length >= 3 ? nombre.Substring(0, 3) : nombre;
            string ape3 = apellido.Length >= 3 ? apellido.Substring(0, 3) : apellido;

            //Junto nombre y apellido
            string nombreyapellido = nom3 + ape3;

            // Verifico que no sea vacíos
            if (string.IsNullOrWhiteSpace(nombreyapellido))
                throw new UsuarioException("Nombre y Apellido no pueden estar vacíos para generar el email.");

            // Genero el mail
            string mail = nombreyapellido + Dominio;

            // Si ya existe, agrego 4 dígitos aleatorios
            var aleatorio = new Random();
            while (RepoUsuario.FindByMail(mail) != null)
            {
                string sufijo = aleatorio.Next(0, 10000).ToString("0000");
                mail = nombreyapellido + sufijo + Dominio;
            }
            Equipo equipo = RepoEquipo.FindById(usuarioDTO.EquipoId);
            Rol rol = RepoRol.FindById(usuarioDTO.RolId);
            // Envio al Mapper
            Usuario usu = MapperUsuario.UsuarioDTOToUsuario(usuarioDTO, mail, equipo, rol);
            RepoUsuario.Add(usu);
            usuarioDTO.Id = usu.Id;
            return usuarioDTO;
        }
    }
}
