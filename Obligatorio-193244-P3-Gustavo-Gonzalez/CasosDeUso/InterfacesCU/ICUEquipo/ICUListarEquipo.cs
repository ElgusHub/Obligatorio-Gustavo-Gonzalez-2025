using CasosDeUso.DTOs.EquipoDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUEquipo
{
    public interface ICUListarEquipo
    {
        public IEnumerable<EquipoDTOs> Ejecutar();
    }
}
