using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    // Generic Interface
    public interface IRepository<T> where T : class
    {
        // Methods such as Get(), GetAll(), Add(), Remove()
        T Get(int id);
        void Add(T entity);
        IEnumerable<T> GetAll();
        void Delete(T entity);
    }
}