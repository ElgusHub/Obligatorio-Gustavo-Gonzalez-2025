using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.TipoGastoDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperMetodoPago
    {
        public static MetodoPago MetodoPagoDTOToMetodoPago(MetodoPagoDTO metodoPagoDTO)
        {
            if (metodoPagoDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new MetodoPago(metodoPagoDTO.Descripcion);
        }


        public static DetalleMetodoPagoDTO MetodoPagoToMetodoPagoDTO(MetodoPago metodoPago)
        {
            if(metodoPago == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new DetalleMetodoPagoDTO()
            {
                Id = metodoPago.Id,
                Descripcion = metodoPago.Descripcion,
            };
        }



        public static IEnumerable<ListadoMetodoPagoDTO> ListMetodoPagoToMetodoPagoDTO(IEnumerable<MetodoPago> metodoPagos)
        {
            List<ListadoMetodoPagoDTO> listadoMetodoPago = new List<ListadoMetodoPagoDTO>();

            foreach (MetodoPago metodoPago in metodoPagos)
            {
                listadoMetodoPago.Add(new ListadoMetodoPagoDTO()
                {
                    Id = metodoPago.Id,
                    Descripcion = metodoPago.Descripcion,
                });
            }
            return listadoMetodoPago;
        }
    }

}
