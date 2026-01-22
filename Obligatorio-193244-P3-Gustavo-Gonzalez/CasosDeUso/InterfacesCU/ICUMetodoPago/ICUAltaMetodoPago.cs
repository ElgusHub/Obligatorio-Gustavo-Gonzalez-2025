using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.PagoUnicoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUMetodoPago
{
    public interface ICUAltaMetodoPago
    {
        void Ejecutar(MetodoPagoDTO metodoPagoDTO);
    }
}
