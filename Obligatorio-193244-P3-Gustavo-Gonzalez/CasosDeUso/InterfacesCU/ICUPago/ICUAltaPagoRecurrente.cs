using CasosDeUso.DTOs.PagoRecurrenteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUPago
{
    public interface ICUAltaPagoRecurrente
    {
        AltaPagoRecurrenteDTO Ejecutar(AltaPagoRecurrenteDTO pagoRecurrente);
    }
}
