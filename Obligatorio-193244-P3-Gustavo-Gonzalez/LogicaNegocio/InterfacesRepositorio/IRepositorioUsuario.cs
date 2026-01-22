using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario:IRepositorio<Usuario>
    {
        Usuario FindByMail(string mail);
        Usuario UsuarioPorMailYPass(string mail, string pass);
        //void CambiarContrasena(Usuario usuario);
    }
}
