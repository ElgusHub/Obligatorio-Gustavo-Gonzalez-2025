using CasosDeUso.DTOs.TipoGastoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUTipoGasto
{
    public interface ICUAltaTipoGasto
    {
        void Ejecutar(TipoGastoDTO tipoGastoDTO);
    }
}
