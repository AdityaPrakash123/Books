using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    // Generic Interface
    public interface IRepository<T> where T : class
    {
        // Methods such as Get(), GetAll(), Add(), Remove()
        
        void Add(T entity);
        void Remove(T entity);

    }
}