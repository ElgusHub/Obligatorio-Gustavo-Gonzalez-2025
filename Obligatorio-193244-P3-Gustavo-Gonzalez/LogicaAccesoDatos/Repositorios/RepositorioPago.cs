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
    public class RepositorioPago : IRepositorioPago
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioPago(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Pago item)
        {
            item.Validar();
            if (FindById(item.Id) == null)
            {
                Contexto.Pagos.Add(item);
                Contexto.SaveChanges();

            }
            else
            {
                throw new PagoException("El pago ya existe en la base");
            }
        }

        public void Delete(Pago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> FindAll()
        {
            var recurrente = Contexto.Pagos
                .OfType<PagoRecurrente>()
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario).Include(u => u.Usuario.Rol)
                .OrderBy(p => p.Id)
            //casteo a Pago
            .ToList<Pago>();

            var unico = Contexto.Pagos
                .OfType<PagoUnico>()
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario).Include(u => u.Usuario.Rol)
                .OrderBy (p => p.Id)
                //casteo a Pago
                .ToList<Pago>();

            // los uno en memoria
            recurrente.AddRange(unico);
            return recurrente;
        }

        public Pago FindById(int id)
        {
            return Contexto.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .Where(p => p.Id == id)
                .SingleOrDefault();
        }

        public void Update(Pago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> BuscarPagosMixtosPorFecha(DateTime fecha)
        {
            var rec = Contexto.Pagos
                .OfType<PagoRecurrente>()
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Where(pr => pr.FechaDesde.Date <= fecha && pr.FechaHasta.Date >= fecha)
                //casteo a Pago
                .ToList<Pago>();

            var uni = Contexto.Pagos
                .OfType<PagoUnico>()
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Where(pu => pu.FechaPago.Value != null && pu.FechaPago.Value == fecha)
                //casteo a Pago
                .ToList<Pago>();

            // los uno en memoria
            rec.AddRange(uni);
            return rec;
        }

        public bool ExisteTipoGasto(int id)
        {
            return Contexto.Pagos
            .AsNoTracking()
            .Any(p => p.TipoGasto.Id == id);
        }

        public IEnumerable<PagoUnico> BuscarPagoUnicoPorMontoMayorA(decimal? valor)
        {
            var pagos = Contexto.Pagos
            .Include(p => p.Usuario)
            .OfType<PagoUnico>()
            .Where(p => p.MontoPago > valor)
            .GroupBy(p => p.Usuario.Id)
            .Select(g => g
                 .OrderByDescending(p => p.FechaPago)
                 .First())
            .ToList();

            // uno por usuario, sin repetidos
            return pagos;
        }

        //RF2
        public IEnumerable<Pago> ObtenerPagoPorUsuario(Usuario usuario)
        {
            var rec = Contexto.Pagos
              .OfType<PagoRecurrente>()
              .Include(p => p.MetodoPago)
              .Include(p => p.TipoGasto)
              .Where(pr => pr.Usuario.Id == usuario.Id)
              //casteo a Pago
              .ToList<Pago>();

            var uni = Contexto.Pagos
                .OfType<PagoUnico>()
                .Include(p => p.MetodoPago)
                .Include(p => p.TipoGasto)
                .Where(pu => pu.Usuario.Id == usuario.Id)
                //casteo a Pago
                .ToList<Pago>();

            // los uno en memoria
            rec.AddRange(uni);
            return rec;
        }

        public IEnumerable<Equipo> BuscarPagosUnicosDeEquiposPorValorMayorA(decimal valor)
        {
            return Contexto.Pagos
            .Include(p => p.Usuario)
            .ThenInclude(u => u.Equipo)
            .OfType<PagoUnico>()
            .Where(p => p.MontoPago > valor)       
            .Select(p => p.Usuario.Equipo)         
            .Distinct()                             
            .OrderByDescending(e => e.Nombre)                
            .ToList();
        }
    }
}
