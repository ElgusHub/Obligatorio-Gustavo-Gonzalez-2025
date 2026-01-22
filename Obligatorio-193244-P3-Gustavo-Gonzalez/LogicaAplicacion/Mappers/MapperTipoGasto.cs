using CasosDeUso.DTOs.TipoGastoDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperTipoGasto
    {
        //Recibo un TipoGastoDTO y convierto a TipoGasto
        public static TipoGasto TipoGastoDTOToTipoGasto(TipoGastoDTO tipogastoDTO)
        {
            if (tipogastoDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new TipoGasto(tipogastoDTO.Nombre, tipogastoDTO.Descripcion);
        }


        public static IEnumerable<ListadoTipoGastoDTO> ListTipoGastoToTipoGastoDTO(IEnumerable<TipoGasto> tipoGastos)
        {
            List<ListadoTipoGastoDTO> listadoTipoGastos = new List<ListadoTipoGastoDTO>();
            
            foreach (TipoGasto tipogasto in tipoGastos)
            {
                listadoTipoGastos.Add(new ListadoTipoGastoDTO()
                {
                    Id = tipogasto.Id,
                    Nombre = tipogasto.Nombre,
                    Descripcion = tipogasto.Descripcion
                });
            }
            return listadoTipoGastos;
        }

        //Recibo un TipoGasto y convierto a TipoGastoDTO
        public static DetalleTipoGastoDTO TipoGastoToDetalleTipoGastoDTO(TipoGasto tipoGasto) 
        {
            if (tipoGasto == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new DetalleTipoGastoDTO()
            {
                Id = tipoGasto.Id,
                Nombre = tipoGasto.Nombre,
                Descripcion = tipoGasto.Descripcion,
            };
        }
    }
}
