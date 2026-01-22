using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicaNegocio.EntidadesNegocio
{
    [Index(nameof(Mail),IsUnique =true)]
    public class Usuario: IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public Rol  Rol { get; set; }
        public Equipo Equipo { get; set; }


        protected Usuario() { }

        public Usuario(string nombre, string apellido, string pass, string mail,Rol rol, Equipo equipo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Password = pass;
            Mail = mail;
            Rol = rol;
            Equipo = equipo;

            Validar();
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarApellido();
            ValidarPassword();
            ValidarMail();
        }

        private void ValidarNombre()
        {
            if (Nombre == null)
            {
                throw new UsuarioException("El nombre no puede ser vacio");
            }
        }

        private void ValidarApellido()
        {
            if(Apellido == null)
            {
                throw new UsuarioException("El apellido no puede ser vacio");
            }
        }

        private void ValidarPassword()
        {
            if(Password == null)
            {
                throw new UsuarioException("El password no puede ser vacio");
            }
            if (Password.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener por lo menos 8 caracteres");
            }
        }

        private void ValidarMail()
        {
            if(Mail == null)
            {
                throw new UsuarioException("El mail no puede ser vacio");
            }
            
        }
    }
}
