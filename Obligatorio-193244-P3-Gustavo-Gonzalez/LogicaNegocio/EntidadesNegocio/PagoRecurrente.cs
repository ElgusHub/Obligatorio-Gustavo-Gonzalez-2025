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
    public class PagoRecurrente : Pago, IValidable
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorCompra { get; set; }

        public PagoRecurrente(MetodoPago metodoPago, TipoGasto tipogasto, Usuario usuario, string descripcion, DateTime fechaDesde, DateTime fechaHasta, decimal valorComrpa)
            : base(metodoPago, tipogasto, usuario, descripcion)
        {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            ValorCompra = valorComrpa;
            Validar();
        }
        protected PagoRecurrente() : base() { }


        //public override DateTime? ObtenerFechaDesde() => FechaDesde;

        public override decimal? ValorCompras()
        {
            return ValorCompra;
        }
        public override DateTime? ObtenerFechaPago()
        {
            return FechaDesde.Date;
        }

        public override decimal CalcularSaldoPendiente()

        {
            // Fecha actual
            DateTime hoy = DateTime.Today;

            // Si todavía no empezó el período, se debe todo
            if (hoy < FechaDesde)
                return (decimal)ValorCompra;

            // Si ya terminó el período, no se debe nada
            if (hoy > FechaHasta)
                return 0m;

            // Calculo total de meses del período
            int totalMeses = ((FechaHasta.Year - FechaDesde.Year) * 12) + (FechaHasta.Month - FechaDesde.Month) + 1;

            // Calculo cuántos meses ya pasaron
            int mesesPagados = ((hoy.Year - FechaDesde.Year) * 12) + (hoy.Month - FechaDesde.Month);

            // Aseguo de que no se pase de rango
            if (mesesPagados > totalMeses)
                mesesPagados = totalMeses;

            // Calculo los meses que faltan
            int mesesRestantes = totalMeses - mesesPagados;

            // Calculo el valor mensual
            decimal valorPorMes = (decimal)(ValorCompra / totalMeses);

            // Calculo saldo pendiente
            decimal saldo = valorPorMes * mesesRestantes;

            // Redondeo
            return Math.Round(saldo, 2);
        }

        public void Validar()
        {
            ValidarFechas();
            ValidarValorCompra();
        }

        //El ingreso minimo de mi fecha sera 01-01-2023 (Validacion personal)
        private static readonly DateTime MinFecha = new DateTime(2023, 1, 1);

        private void ValidarFechas()
        {
            var desde = FechaDesde.Date;
            var hasta = FechaHasta.Date;
            var hoy = DateTime.Today;

            
            if (desde < MinFecha)
                throw new PagoException("La Fecha Desde no puede ser anterior a 01/01/2023.");

            if (hasta < MinFecha)
                throw new PagoException("La Fecha Hasta no puede ser anterior a 01/01/2023.");

            if (hasta <= desde)
                throw new PagoException("La Fecha Hasta debe ser posterior a la Fecha Desde.");

            if (hasta < hoy)
                throw new PagoException("La Fecha Hasta no puede ser anterior a hoy.");

        }
        private void ValidarValorCompra()
        {
            if (ValorCompra <= 0m)
                throw new PagoException("El valor de compra debe ser mayor a 0.");
        }
    }
}
