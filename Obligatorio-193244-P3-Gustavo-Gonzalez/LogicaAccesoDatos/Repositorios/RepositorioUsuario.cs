using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        //INYECCION DE DEPENDENCIA DE EMPRESACONTEXTO
        public EmpresaContexto Contexto { get; set; }
        public RepositorioUsuario(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Usuario item)
        {
            item.Validar();
            Usuario usu = FindByMail(item.Mail);
            if (usu == null)
            {
                Contexto.Usuarios.Add(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new UsuarioException("El Usuario ya existe");
            }
        }

        public Usuario FindByMail(string mail)
        {
            return Contexto.Usuarios
                .Include(u=>u.Equipo)
                .Include(u => u.Rol)
                .Where(u => u.Mail == mail)
                .SingleOrDefault();
        }

        public void Delete(Usuario item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios.ToList();

        }

        public Usuario FindById(int id)
        {
            return Contexto.Usuarios
                .Include(u=>u.Equipo)
                .Include(u => u.Rol)
                .Where(u => u.Id == id)
                .SingleOrDefault();
        }

        public void Update(Usuario item)
        {
            Usuario usuario = FindById(item.Id);
            if (usuario == null)
                throw new UsuarioException("El usuario no existe");

            item.Validar();
            Contexto.SaveChanges();
        }

        public Usuario UsuarioPorMailYPass(string mail, string pass)
        {
            return Contexto.Usuarios
                .Include(u=>u.Rol)
                .Include(u=>u.Equipo)
                .Where(u => u.Mail.Equals(mail)&& u.Password.Equals(pass))
                .SingleOrDefault();
        }

    }
}
