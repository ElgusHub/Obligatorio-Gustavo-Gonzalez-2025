using CasosDeUso.DTOs.PagoRecurrenteDTO;
using CasosDeUso.DTOs.PagoUnicoDTO;
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
    public class CUAltaPagoRecurrente : ICUAltaPagoRecurrente
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioMetodoPago RepoMetodoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }

        public CUAltaPagoRecurrente(IRepositorioPago repoPago, IRepositorioMetodoPago repoMetodoPago, IRepositorioUsuario repoUsuario, IRepositorioTipoGastos repoTipoGasto)
        {
            RepoPago = repoPago;
            RepoMetodoPago = repoMetodoPago;
            RepoUsuario = repoUsuario;
            RepoTipoGasto = repoTipoGasto;
        }

        public AltaPagoRecurrenteDTO Ejecutar(AltaPagoRecurrenteDTO pagoRecurrenteDTO)
        {
            Usuario usuario = RepoUsuario.FindByMail(pagoRecurrenteDTO.UsuarioMail);
            TipoGasto tipoGasto = RepoTipoGasto.FindById(pagoRecurrenteDTO.TipoGastoId);
            MetodoPago metodoPago = RepoMetodoPago.FindById(1);

            if (pagoRecurrenteDTO != null)
            {
                if (usuario is null)
                {
                    throw new PagoException("Ususario no encontrado");
                }
                if (tipoGasto is null)
                {
                    throw new PagoException("Tipo de gasto no encontrado");
                }
                if (metodoPago is null)
                {
                    throw new PagoException("Metodo de pago no encontrado");
                }
                if (pagoRecurrenteDTO.ValorCompra <= 0)
                {
                    throw new PagoException("El valor de compra debe ser un monto mayor a cero");
                }
            }
            else
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            PagoRecurrente recurrente = MapperPago.PagoRecurrenteDTOToPagoRecuerrente(pagoRecurrenteDTO, usuario, tipoGasto, metodoPago);
            RepoPago.Add(recurrente);
            return pagoRecurrenteDTO;
        }
    }
}
