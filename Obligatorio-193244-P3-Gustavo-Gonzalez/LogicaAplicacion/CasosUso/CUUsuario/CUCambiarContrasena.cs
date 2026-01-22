using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.ICUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUCambiarContrasena : ICUCambiarContrasena
    {

        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUCambiarContrasena(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public string Ejecutar(CambiarContrasenaDTO cambiarContrasenaDTO)
        {
            Usuario usuario = RepoUsuario.FindByMail(cambiarContrasenaDTO.Mail);
            if(usuario == null)
            {
                throw new UsuarioException("No existe usuario con ese id");
            }
            string passAnterior = usuario.Password.Trim();
            string nuevaPassword = GenerarPassword(8); 
            if(string.IsNullOrEmpty(nuevaPassword))
            {
                throw new UsuarioException("La contraseña no se pudo generar correctamente");
            }
            if (nuevaPassword == usuario.Password)
            {
                throw new UsuarioException("La contraseña actual no debe ser igual a la nueva contraseña");
            }
            usuario.Password = nuevaPassword; 

            RepoUsuario.Update(usuario); 

            return "Contraseña cambiada con éxito al usuario " + usuario.Nombre + ", contraseña anterior: " + passAnterior + ", nueva contraseña: " + nuevaPassword;
        }


        private string GenerarPassword(int longitud = 8)
        {
            const string mayus = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minus = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string especiales = "!@#$%&/?*+-_";

            string todos = mayus + minus + numeros + especiales;

            var password = new StringBuilder();
            byte[] randomBytes = new byte[longitud];

            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < longitud; i++)
            {
                int index = randomBytes[i] % todos.Length;
                password.Append(todos[index]);
            }

            return password.ToString();
        }

    }
}
