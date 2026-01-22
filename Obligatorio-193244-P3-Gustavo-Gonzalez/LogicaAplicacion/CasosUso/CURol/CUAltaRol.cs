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
    public class CUAltaRol : ICUAltaRol
    {
        public IRepositorioRol RepoRol { get; set; }
        public CUAltaRol(IRepositorioRol repoRol)
        {
            RepoRol = repoRol;
        }


        public void Ejecutar(AltaRolDTO rolDTO)
        {
            if (rolDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            Rol rol = MapperRol.RolDTOToRol(rolDTO);
            RepoRol.Add(rol);
        }
    }
}
