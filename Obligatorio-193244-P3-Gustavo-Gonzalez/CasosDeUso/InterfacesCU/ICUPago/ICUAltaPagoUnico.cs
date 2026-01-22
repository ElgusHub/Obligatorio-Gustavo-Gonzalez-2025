using CasosDeUso.DTOs.PagoUnicoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUPago
{
    public interface ICUAltaPagoUnico
    {
        AltaPagoUnicoDTO Ejecutar(AltaPagoUnicoDTO pagoUnicoDTO);
    }
}
