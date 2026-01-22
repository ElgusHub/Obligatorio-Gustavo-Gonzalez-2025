using CasosDeUso.DTOs.TipoGastoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUTipoGasto
{
    public interface ICUBuscarTipoGasto
    {
        DetalleTipoGastoDTO Ejecutar(int id);
    }
}
