using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        T FindById(int id);
        IEnumerable<T> FindAll();
    }
}
