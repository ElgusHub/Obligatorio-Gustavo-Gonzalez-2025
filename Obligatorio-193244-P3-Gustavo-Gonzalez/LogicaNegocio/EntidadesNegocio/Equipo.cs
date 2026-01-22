using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicaNegocio.EntidadesNegocio
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Equipo:IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        protected Equipo() { }
        public Equipo(string nombre)
        {
            Nombre = nombre;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new EquipoException("El nombre de equipo no puede ser vacio");
            }
        }
    }
}
