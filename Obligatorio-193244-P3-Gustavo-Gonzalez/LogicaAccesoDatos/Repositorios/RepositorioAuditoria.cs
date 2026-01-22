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
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioAuditoria(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Auditoria item)
        {
            item.Validar();
            Contexto.Add(item);
            Contexto.SaveChanges();
        }

        public void Update(Auditoria item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Auditoria item)
        {
            throw new NotImplementedException();
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> ObtenerAuditoriaPorIdTipoGasto(int id)
        {
            return Contexto.Auditorias
                .Include(a=>a.TipoGasto)
                .Include(a=>a.Usuario)
                .ThenInclude(u=>u.Rol)
                .Where(a=>a.TipoGasto.Id == id && a.Activo ==1)
                .OrderByDescending(a => a.FechaUtc) 
                .ToList();
        }
    }
}
