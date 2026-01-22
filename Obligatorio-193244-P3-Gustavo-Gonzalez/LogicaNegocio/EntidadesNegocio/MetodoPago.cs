using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.EntidadesNegocio
{
    [Index(nameof(Descripcion), IsUnique = true)]
    public class MetodoPago:IValidable
    {
        public int Id { get; set; }
        public string Descripcion { get; private set; }

        public MetodoPago(string descripcion)
        {
            Descripcion = descripcion;
        }

        protected MetodoPago() { }


        public void Validar()
        {
            ValidarDescripcion();

        }

        private void ValidarDescripcion()
        {
            if(Descripcion == null)
            {
                throw new MetodoPagoException("La descripcion no puede ser vacia");
            }
        }
    }
}
