using CasosDeUso.DTOs.MetodoPagoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUMetodoPago
{
    public interface ICUBucarMetodoPago
    {
        DetalleMetodoPagoDTO Ejecutar(int id);
    }
}
