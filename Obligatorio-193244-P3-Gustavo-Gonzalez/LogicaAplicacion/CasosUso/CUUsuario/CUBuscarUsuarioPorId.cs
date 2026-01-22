using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.ICUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUBuscarUsuarioPorId : ICUBuscarUsuarioPorId
    {
        public IRepositorioUsuario RepoUsuraio {  get; set; }

        public CUBuscarUsuarioPorId(IRepositorioUsuario repo)
        {
            RepoUsuraio = repo;
        }



        public ListadoUsuarioPorIdDTO Ejecutar(int id)
        {
            Usuario us = RepoUsuraio.FindById(id);

            if (us == null)
            {
                throw new UsuarioException("No existe un usuario con ese Id");
            }
            return MapperUsuario.BuscarUsuarioPorIdToBuscarUsuarioPorIdDTO(us);
        }
    }
}
