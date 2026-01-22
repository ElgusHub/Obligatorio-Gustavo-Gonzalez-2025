using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.TipoGastoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUListadoMetodoPago
{
    public interface ICUListadoMetodoPago
    {
        public IEnumerable<ListadoMetodoPagoDTO> Ejecutar();
    }
}
