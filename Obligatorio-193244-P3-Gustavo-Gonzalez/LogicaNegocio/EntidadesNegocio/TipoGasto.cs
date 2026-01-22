using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    [Index(nameof(Nombre), IsUnique = true, Name = "UX_TipoGasto_Nombre")]
    public class TipoGasto : IValidable
    {

        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoGasto(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Validar();
        }


        protected TipoGasto(){}


        public void Validar()
        {
            ValidarNombreVacio();
            ValidarDescripcionVacia();
        }

        private void ValidarNombreVacio()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new GastoException("El nombre es obligatorio");
            }
        }

        private void ValidarDescripcionVacia()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new GastoException("La descripcion es obligatoria");
            }
        }

    }
}
