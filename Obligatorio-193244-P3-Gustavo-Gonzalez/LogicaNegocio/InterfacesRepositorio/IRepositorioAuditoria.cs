using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAuditoria:IRepositorio<Auditoria>
    {
        IEnumerable<Auditoria> ObtenerAuditoriaPorIdTipoGasto(int id);
    }
}
