using ExcepcionesPropias.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Rol(string nombre)
        {
            Nombre = nombre;
            Validar();
        }

        protected Rol(){}


        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new RolException("El nombre del Rol no puede ser vacio");
            }
        }
    }
}
