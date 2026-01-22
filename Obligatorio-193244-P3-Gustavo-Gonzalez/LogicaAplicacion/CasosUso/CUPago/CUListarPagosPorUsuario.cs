using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.InterfacesCU.ICUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUListarPagosPorUsuario:ICUListarPagosPorUsuario
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }

        public CUListarPagosPorUsuario(IRepositorioPago repoPago, IRepositorioUsuario repoUsuario)
        {
            RepoPago = repoPago;
            RepoUsuario = repoUsuario;
        }

        public IEnumerable<ListaPagosPorUsuarioDTO> Ejecutar(string email)
        {
            Usuario usuario = RepoUsuario.FindByMail(email);
            if(usuario == null)
            {
                throw new PagoException("El ususario no exite");
            }
            IEnumerable<Pago> pagos = RepoPago.ObtenerPagoPorUsuario(usuario);
            if (pagos == null || pagos.Count() == 0)
            {
                throw new PagoException("No se encontraron pagos de su usuario");
            }
            return MapperPago.PagosPorUsuarioToPagosPorUsuarioDTO(pagos);
        }
    }
}
