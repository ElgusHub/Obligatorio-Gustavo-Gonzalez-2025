using CasosDeUso.DTOs.AuditoriaDTO;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperAuditoria
    {
        public static Auditoria AuditoriaDTOToAuditoria(AuditoriaDTO dto, Usuario usu, TipoGasto tipoG)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Datos del dto incorrectos");
            }
            if(usu == null)
            {
                throw new ArgumentNullException("Datos del ususario incorrectos");
            }
            if (tipoG == null)
            {
                throw new ArgumentNullException("Datos del tipo gasto incorrectos");
            }
            return Auditoria.Crear(
                accion: dto.Accion,
                ususario: usu,
                tipoGasto: tipoG,
                fechaUtc: dto.Fecha
            );
        }

        public static IEnumerable<AuditoriaDTO> AuditoriaPorIdTipoGastoToAuditoriaPorIdTipoGastoDTO(IEnumerable<Auditoria> auditorias)
        {
            List<AuditoriaDTO> listaAuditoria = new List<AuditoriaDTO>();

            if(auditorias == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            foreach(Auditoria auditoria in auditorias)
            {
                listaAuditoria.Add(new AuditoriaDTO()
                {
                    Accion = auditoria.Accion,
                    Fecha = auditoria.FechaUtc,
                    UsuarioId = auditoria.Usuario.Id,
                    NombreUsuario = auditoria.Usuario.Nombre,

                    TipoGastoId = auditoria.TipoGasto.Id,
                    TipoGastoNombre = auditoria.TipoGasto.Nombre,
                    DescripcionTipoGasto = auditoria.TipoGasto.Descripcion,
                    Activo = auditoria.Activo,
                }
                );
            }
            return listaAuditoria;
        }

    }
}
