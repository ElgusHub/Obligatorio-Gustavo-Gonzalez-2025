using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Auditoria : IValidable
    {
        public int Id { get; private set; }

        public string Accion { get; set; }

        
        public DateTime FechaUtc { get; private set; }

        //public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        //public string NombreUsuario { get; set; } 
        //public string TipoGastoNombre { get; private set; }
        public TipoGasto TipoGasto { get; private set; }

        //public string TipoGastoDesc { get; set; }
        public int Activo { get; set; } = 1;

        // Requerido por EF
        protected Auditoria() { }


        public static Auditoria Crear(string accion, Usuario ususario, TipoGasto tipoGasto, DateTime fechaUtc)
        {
            var a = new Auditoria
            {
                Accion = accion,
                Usuario = ususario,
                TipoGasto = tipoGasto,
                FechaUtc = fechaUtc,
            };

            a.Validar();
            return a;
        }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Accion))
                throw new AuditoriaException("Acción de auditoría inválida");

            if (FechaUtc == default)
                throw new AuditoriaException("La fecha no puede ser el valor por defecto");

            if (Usuario == null)
                throw new AuditoriaException("El usuario no puede estar vacio");
            if (TipoGasto == null)
            {
                throw new AuditoriaException("El Tipo de Gasto no puede estar vacio");
            }
        }
    }
}











