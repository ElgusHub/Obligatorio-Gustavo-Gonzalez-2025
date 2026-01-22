using CasosDeUso.DTOs.RolDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperRol
    {
        public static Rol RolDTOToRol(AltaRolDTO rolDTO)
        {
            if (rolDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new Rol(rolDTO.NombreRol);
        }

        public static IEnumerable<ListarRolParaAltaUsuarioDTO> ListadoRolToListadoRolDTO(IEnumerable<Rol> rol)
        {
            List<ListarRolParaAltaUsuarioDTO> listadoRol = new List<ListarRolParaAltaUsuarioDTO>();

            foreach (Rol r in rol)
            {
                if (r.Nombre != "Administrador")
                {

                    listadoRol.Add(new ListarRolParaAltaUsuarioDTO()
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                    });
                }

            }
            return listadoRol;
        }


        public static IEnumerable<ListadoRolParaLoginDTO> ListadoRolLoginToListadoRolLoginDTO(IEnumerable<Rol> rol)
        {
            List<ListadoRolParaLoginDTO> listadoRol = new List<ListadoRolParaLoginDTO>();

            foreach (Rol r in rol)
            {
                listadoRol.Add(new ListadoRolParaLoginDTO()
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                });
            }
            return listadoRol;
        }


    }
}
