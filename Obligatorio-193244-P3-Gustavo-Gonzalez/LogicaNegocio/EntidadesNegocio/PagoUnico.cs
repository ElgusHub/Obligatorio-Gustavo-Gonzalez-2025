using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class PagoUnico : Pago, IValidable
    {
        public int? NumeroRecibo { get; set; }
        public DateTime? FechaPago { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MontoPago { get; set; }


        public PagoUnico(MetodoPago metodoPago, TipoGasto tipogasto, Usuario usuario, string descripcion, int nunrecibo, DateTime fechaPago, decimal montopago)
            :base(metodoPago, tipogasto, usuario, descripcion)
        {
            NumeroRecibo = nunrecibo;
            FechaPago = fechaPago;
            MontoPago = montopago;
            Validar();
        }
        public override decimal CalcularSaldoPendiente() => 0m;
        public override DateTime? ObtenerFechaPago()
        {
            return FechaPago;
        }
        protected PagoUnico():base() { }

        public override decimal? ValorCompras()
        {
            return MontoPago;
        }

        public void Validar()
        {
            ValidarNumRecibo();
            ValidarFechaPago();
            ValidarMontoPago();
        }

        private void ValidarNumRecibo()
        {
            if (NumeroRecibo == 0)
            {
                throw new PagoException("El campo tiene que contener un numero de recibo");
            }
            if(NumeroRecibo == null)
            {
                throw new PagoException("El numero de recibo no puede ser vacio");
            }

        }

        private void ValidarFechaPago()
        {
            if(FechaPago == null)
            {
                throw new PagoException("La fecha de pago no puede ser vacia");
            }
        }

        private void ValidarMontoPago()
        {
            if (!MontoPago.HasValue || MontoPago.Value <= 0m)
            {
                throw new PagoException("El Monto de pago debe ser mayor a 0.");
            }
        }

        
    }
}
