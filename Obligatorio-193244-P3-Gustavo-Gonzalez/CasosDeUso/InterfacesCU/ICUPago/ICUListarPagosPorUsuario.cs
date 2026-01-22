using CasosDeUso.DTOs.PagoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUPago
{
    public interface ICUListarPagosPorUsuario
    {
        IEnumerable<ListaPagosPorUsuarioDTO> Ejecutar(string email);
    }
}
