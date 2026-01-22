using LogicaNegocio.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos
{
    public class EmpresaContexto:DbContext
    {
        public EmpresaContexto(DbContextOptions options) : base(options){}

        public DbSet<TipoGasto> TiposGastos {  get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PagoUnico> PagoUnicos { get; set; }
        public DbSet<PagoRecurrente> PagoRecurrentes { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Pago>().HasOne(p => p.TipoGasto).WithMany().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Pago>().HasOne(p => p.Usuario).WithMany().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Pago>().HasOne(p => p.MetodoPago).WithMany().OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }


    }
}
