using CasosDeUso.DTOs.EquipoDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperEquipo
    {
        public static IEnumerable<EquipoDTOs> ListEquipoToListEquipoDTO(IEnumerable<Equipo> equipoDTO)
        {
            List<EquipoDTOs> listadoEquipo = new List<EquipoDTOs>();
            if (equipoDTO == null)
            {
                throw new ArgumentNullException("El equipo no existe");
            }
            foreach (Equipo equipo in equipoDTO)
            {
                listadoEquipo.Add(new EquipoDTOs()
                {
                    Nombre = equipo.Nombre,
                    Id = equipo.Id,
                });
            }
            return listadoEquipo;
        }
    }
}
