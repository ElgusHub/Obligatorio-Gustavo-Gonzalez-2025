using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class MetodoPagoException:Exception
    {
        public MetodoPagoException() { }
        public MetodoPagoException(string message) : base(message) { }
        public MetodoPagoException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
