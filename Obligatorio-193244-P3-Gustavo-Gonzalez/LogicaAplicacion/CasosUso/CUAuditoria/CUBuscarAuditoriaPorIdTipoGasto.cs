using CasosDeUso.DTOs.AuditoriaDTO;
using CasosDeUso.InterfacesCU.ICUAuditoria;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUBuscarAuditoriaPorIdTipoGasto : ICUBuscarAuditoriaPorIdTipoGasto
    {
        public IRepositorioAuditoria RepoAuditoria { get; set; }

        public CUBuscarAuditoriaPorIdTipoGasto(IRepositorioAuditoria repoAuditoria) 
        {
            RepoAuditoria = repoAuditoria;
        }



        public IEnumerable<AuditoriaDTO> Ejecutar(int id)
        {
            IEnumerable<Auditoria> auditoria = RepoAuditoria.ObtenerAuditoriaPorIdTipoGasto(id);

            if(auditoria == null || auditoria.Count()==0)
            {
                throw new AuditoriaException("No se encontraron auditorias con el id de tipo de gastos");
            }
            return MapperAuditoria.AuditoriaPorIdTipoGastoToAuditoriaPorIdTipoGastoDTO(auditoria);
        }
    }
}
