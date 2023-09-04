using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product> 
    {
        Product Get(Expression<Func<Product,bool>> filter);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Save();
    }
}
