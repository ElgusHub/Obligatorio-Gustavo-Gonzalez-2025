using CasosDeUso.DTOs.MetodoPagoDTO;
using CasosDeUso.DTOs.PagoDTO;
using CasosDeUso.DTOs.PagoRecurrenteDTO;
using CasosDeUso.DTOs.PagoUnicoDTO;
using CasosDeUso.DTOs.TipoGastoDTOs;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using CasosDeUso.DTOs.PagoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperPago
    {
        public static PagoUnico PagoUnicoDTOToPagoUnico(AltaPagoUnicoDTO pagoUnicoDTO, Usuario ususario, TipoGasto tipoGasto, MetodoPago metodoPago)
        {
            return new PagoUnico(metodoPago, tipoGasto, ususario, pagoUnicoDTO.Descripcion, pagoUnicoDTO.NumeroRecibo, pagoUnicoDTO.FechaPago, pagoUnicoDTO.MontoPago);
        }

        public static PagoRecurrente PagoRecurrenteDTOToPagoRecuerrente(AltaPagoRecurrenteDTO pagoRecurrenteDTO, Usuario ususario, TipoGasto tipoGasto, MetodoPago metodoPago)
        {
            if (pagoRecurrenteDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new PagoRecurrente(metodoPago, tipoGasto, ususario, pagoRecurrenteDTO.Descripcion, pagoRecurrenteDTO.FechaDesde, pagoRecurrenteDTO.FechaHasta, pagoRecurrenteDTO.ValorCompra);
        }


        public static List<ListaFiltroPagoXFecha> PagoUnicoFiltradoToPagoUnicoFiltradoDTO(IEnumerable<Pago> Pago)
        {
            List<ListaFiltroPagoXFecha> listaFiltro = new List<ListaFiltroPagoXFecha>();

            foreach(Pago pagoTotales in Pago)
            {
                listaFiltro.Add(new ListaFiltroPagoXFecha()
                {
                    Id = pagoTotales.Id,
                    NombreMetodoPago = pagoTotales.MetodoPago.Descripcion,
                    NombreTipoGasto = pagoTotales.TipoGasto.Nombre,
                    NombreGasto = pagoTotales.Descripcion,
                    FechaCompra = (DateTime)pagoTotales.ObtenerFechaPago(),
                    ValorCompra = (decimal)pagoTotales.ValorCompras(),
                    SaldoPendiente = pagoTotales.CalcularSaldoPendiente(),
                });
            }
            return listaFiltro;
        }


        public static List<ListadoPagoDTO> ListadoPagoToListadoPagoDTO(IEnumerable<Pago> pagos)
        {
            List<ListadoPagoDTO> listaPagos = new List<ListadoPagoDTO>();

            foreach (Pago p in pagos)
            {
                listaPagos.Add(new ListadoPagoDTO()
                {
                    IdPago = p.Id,
                    MetodoPago = p.MetodoPago.Descripcion,
                    TipoPago = p.TipoGasto.Nombre,
                    DescripcionPago = p.TipoGasto.Descripcion,
                    NombreUsuario = p.Usuario.Nombre,
                    RolUsuario = p.Usuario.Rol.Nombre,
                    DescTipoPago = p.Descripcion,
                    Montopago = p.ValorCompras(),
                });
            }
                return listaPagos;
        }

        public static IEnumerable<ListaPagoUnicoMayorADTO> PagoUnicoToPagoUnicoDTO(IEnumerable<PagoUnico> pagos)
        {
            List<ListaPagoUnicoMayorADTO> listaPagosMayorA = new List<ListaPagoUnicoMayorADTO>();

            foreach (PagoUnico pagoU in pagos)
            {
                listaPagosMayorA.Add(new ListaPagoUnicoMayorADTO()
                {
                    IdPgo = pagoU.Id,
                    DescripcionPago = pagoU.Descripcion,
                    NombreUsuario = pagoU.Usuario.Nombre,
                    ValorCompra = pagoU.MontoPago
                });
            }
            return listaPagosMayorA;
        }

        public static BuscarPagoPorIdDTO BuscarPagoPorIdToBuscarPagoPorIdDTO(Pago pago)
        {
            if (pago == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }
            return new BuscarPagoPorIdDTO()
            {
                IdPago = pago.Id,
                MetodoPago = pago.MetodoPago.Descripcion,
                TipoPago = pago.TipoGasto.Nombre,
                UsuarioId = pago.Usuario.Id,
                NombreUsuario = pago.Usuario.Nombre,
                DescripcionPago = pago.Descripcion,
                MontoPago = pago.ValorCompras()
            };
        }

        public static List<ListaPagosPorUsuarioDTO> PagosPorUsuarioToPagosPorUsuarioDTO(IEnumerable<Pago> pago)
        {
            List<ListaPagosPorUsuarioDTO> listaPagos = new List<ListaPagosPorUsuarioDTO>();

            if (pago == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }

            foreach (Pago pagoTotales in pago)
            {
                listaPagos.Add(new ListaPagosPorUsuarioDTO()
                {
                    Id = pagoTotales.Id,
                    NombreUsuario = pagoTotales.Usuario.Nombre.ToString(),
                    NombreMetodoPago = pagoTotales.MetodoPago.Descripcion,
                    NombreTipoGasto = pagoTotales.TipoGasto.Nombre,
                    NombreGasto = pagoTotales.Descripcion,
                    FechaCompra = (DateTime)pagoTotales.ObtenerFechaPago(),
                    ValorCompra = (decimal)pagoTotales.ValorCompras(),
                    SaldoPendiente = pagoTotales.CalcularSaldoPendiente(),
                });
            }
            return listaPagos;
        }

        public static IEnumerable<EquiposPorPagosDTO> ListaEquipoMontoMayorAToListaEquipoMontoMayorADTO(IEnumerable<Equipo> equipo)
        {
            List<EquiposPorPagosDTO> listaEquipos = new List<EquiposPorPagosDTO>();
            foreach (Equipo equipos in equipo)
            {
                listaEquipos.Add(new EquiposPorPagosDTO()
                {
                    EquipoId = equipos.Id,
                    EquipoNombre = equipos.Nombre
                }
                );
            }
            return listaEquipos;
        }

    }
}
