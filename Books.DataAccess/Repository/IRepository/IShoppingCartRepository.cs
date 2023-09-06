using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter);
        IEnumerable<ShoppingCart> GetAll();
        void Update(ShoppingCart shoppingCart);
        void Save();
    }
}