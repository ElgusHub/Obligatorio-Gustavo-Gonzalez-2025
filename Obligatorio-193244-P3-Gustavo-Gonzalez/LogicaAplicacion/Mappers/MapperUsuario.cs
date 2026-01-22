using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.DTOs.TipoGastoDTOs;
using CasosDeUso.DTOs.UsuarioDTOs;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperUsuario
    {
        //Recibo un UsuarioDTO y convierto a Usuario
        public static Usuario UsuarioDTOToUsuario(AltaUsuarioDTO usuarioDTO, string mail, Equipo equipo, Rol rol)
        {
            if (usuarioDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Usuario(usuarioDTO.Nombre, usuarioDTO.Apellido, usuarioDTO.Pass, mail, rol, equipo);
        }


        //Recibo un usuario y convierto a usuarioDTO
        public static UsuarioLogueadoDTO UsuarioLoginDTOToUsuarioLogin(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new UsuarioLogueadoDTO()
            {
                Id = usuario.Id,
                Mail = usuario.Mail,
                Rol = usuario.Rol.Nombre,
            };

        }

        public static IEnumerable<ListadoUsuarioDTO> ListUsuarioToListUsusarioDTO(IEnumerable<Usuario> usuarios)
        {
            List<ListadoUsuarioDTO> listadoUsuarios = new List<ListadoUsuarioDTO>();

            foreach (Usuario usario in usuarios)
            {
                listadoUsuarios.Add(new ListadoUsuarioDTO()
                {
                    Id = usario.Id,
                    Nombre = usario.Nombre,
                    Mail = usario.Mail,

                });
            }
            return listadoUsuarios;
        }


        public static ListadoUsuarioPorIdDTO BuscarUsuarioPorIdToBuscarUsuarioPorIdDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new ListadoUsuarioPorIdDTO()
            {
                Id = usuario.Id,
                //Nombre = usuario.Nombre,
                //Apellido = usuario.Apellido,
                Mail = usuario.Mail,
            };
        }
    }
}
