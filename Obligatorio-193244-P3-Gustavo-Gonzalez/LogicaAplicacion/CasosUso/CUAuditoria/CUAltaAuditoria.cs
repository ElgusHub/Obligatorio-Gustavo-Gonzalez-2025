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
    public class CUAltaAuditoria:ICUAltaAuditoria
    {
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }
        
        public CUAltaAuditoria(IRepositorioAuditoria repoAuditoria, IRepositorioUsuario repoUsuario, IRepositorioTipoGastos repoTipoGasto)
        {
            RepoAuditoria = repoAuditoria;
            RepoUsuario = repoUsuario;
            RepoTipoGasto = repoTipoGasto;
            
        }

        public void Ejecutar(AuditoriaDTO auditoriaDTO)
        {
            if (auditoriaDTO == null)
            {
                throw new AuditoriaException("Datos incorrectos");
            }
            Usuario ususario = RepoUsuario.FindById(auditoriaDTO.UsuarioId);
            TipoGasto tipoGasto = RepoTipoGasto.FindById(auditoriaDTO.TipoGastoId);
            
            if (ususario == null)
            {
                throw new AuditoriaException("No existe ususario con ese id");
            }
            if(tipoGasto == null)
            {
                throw new AuditoriaException("No existe Tipo de Gasto con ese id");
            }

            Auditoria auditoria = MapperAuditoria.AuditoriaDTOToAuditoria(auditoriaDTO, ususario, tipoGasto);
            RepoAuditoria.Add(auditoria);
        }
    }
}
