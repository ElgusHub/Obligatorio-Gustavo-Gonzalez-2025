using CasosDeUso.DTOs.AuditoriaDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUAuditoria
{
    public interface ICUAltaAuditoria
    {
        void Ejecutar(AuditoriaDTO auditoriaDTO);
    }
}
