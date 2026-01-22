using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias.ExcepcionesEntidades;

namespace LogicaNegocio.EntidadesNegocio
{
    public abstract class Pago: IValidable
    {
        public int Id { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public Usuario Usuario { get; set; }
        public string Descripcion { get; set; }


        protected Pago(){}

        public Pago(MetodoPago metodoPago, TipoGasto tipogasto, Usuario usuario, string descripcion)
        {
            MetodoPago = metodoPago;
            TipoGasto = tipogasto;
            Usuario = usuario;
            Descripcion = descripcion;

            Validar();
        }

        public abstract decimal CalcularSaldoPendiente();
        public abstract decimal? ValorCompras();
        public abstract DateTime? ObtenerFechaPago();

        public void Validar()
        {
            ValidarMetodoPago();
            ValidarTipoGasto();
            ValidarUsuario();
            ValidarDescripcion();
        }

        private void ValidarMetodoPago() 
        {
            if( MetodoPago is null)
            {
                throw new PagoException("El método de pago no puede ser vaco");
            }
        }

        private void ValidarTipoGasto()
        {
            if(TipoGasto is null)
            {
                throw new PagoException("El Tipo de gasto no puede ser vacio");
            }
        }
        private void ValidarUsuario()
        {
            if (Usuario is null)
            {
                throw new PagoException("El usuario no puede ser vacío");
            }
        }
        private void ValidarDescripcion() 
        {
            if(string.IsNullOrEmpty( Descripcion))
            {
                throw new PagoException("La descripción no puede ser vacía");
            }
        }
    }

    
}
