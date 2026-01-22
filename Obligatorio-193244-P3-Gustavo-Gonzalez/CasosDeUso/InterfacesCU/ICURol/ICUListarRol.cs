using CasosDeUso.DTOs.RolDTO;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICURol
{
    public interface ICUListarRol
    {
        public IEnumerable<ListarRolParaAltaUsuarioDTO> Ejecutar();
    }
}
