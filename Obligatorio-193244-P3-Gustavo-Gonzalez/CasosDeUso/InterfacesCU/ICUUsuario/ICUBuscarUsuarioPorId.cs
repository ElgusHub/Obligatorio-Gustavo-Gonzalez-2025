using CasosDeUso.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.ICUUsuario
{
    public interface ICUBuscarUsuarioPorId
    {
        ListadoUsuarioPorIdDTO Ejecutar(int id);
    }
}
