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
    public class CUAltaPagoUnico : ICUAltaPagoUnico
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioTipoGastos RepoTipoGasto { get; set; }
        public IRepositorioMetodoPago RepoMetodoPago { get; set; }
        public CUAltaPagoUnico(IRepositorioPago repoPago, IRepositorioUsuario repositorioUsuario, IRepositorioTipoGastos tipoGastos, IRepositorioMetodoPago metodoPago)
        {
            RepoPago = repoPago;
            RepoUsuario = repositorioUsuario;
            RepoTipoGasto = tipoGastos;
            RepoMetodoPago = metodoPago;
        }

        public AltaPagoUnicoDTO Ejecutar(AltaPagoUnicoDTO pagoUnicoDTO)
        {
            //Me queda la duda si hacer el metodoPago como valueObject
            Usuario usuario = RepoUsuario.FindByMail(pagoUnicoDTO.UsuarioMail);
            TipoGasto tipoGasto = RepoTipoGasto.FindById(pagoUnicoDTO.TipoGastoId);
            MetodoPago metodoPago = RepoMetodoPago.FindById(2);
            //string descripcion = pagoUnicoDTO.Descripcion;

            if (usuario is null)
            {
                throw new ArgumentNullException("Ususario no encontrado");
            }
            if (tipoGasto is null)
            {
                throw new ArgumentNullException("Tipo de gasto no encontrado");
            }
            if (metodoPago is null)
            {
                throw new ArgumentNullException("Metodo de pago no encontrado");
            }
            //if (string.IsNullOrEmpty(descripcion))
            //{
            //    throw new PagoException("Descripcion no encontrada");
            //}
            if (string.IsNullOrEmpty(pagoUnicoDTO.Descripcion))
            {
                throw new ArgumentNullException("La descripción no puede ser nula");
            }
            if (pagoUnicoDTO.NumeroRecibo <= 0)
            {
                throw new PagoException("El número de recibo no puede ser cero o un número negativo");
            }
            if (pagoUnicoDTO.MontoPago <= 0)
            {
                throw new PagoException("El monto de pago no puede ser cero");
            }
            var min = new DateTime(2023, 1, 1);   // 01/01/2023
            if (pagoUnicoDTO.FechaPago.Date <= min)
            {
                throw new PagoException("La fecha debe ser posterior a 01/01/2023");
            }
            PagoUnico pagoUnico = MapperPago.PagoUnicoDTOToPagoUnico(pagoUnicoDTO, usuario, tipoGasto, metodoPago);
            RepoPago.Add(pagoUnico);
            return pagoUnicoDTO;
        }
    }
}
