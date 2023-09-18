using Books.DataAccess.Data;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter)
        {
            IQueryable<ShoppingCart> query = _db.ShoppingCarts;
            query = query.AsNoTracking();
            return query.FirstOrDefault(filter);
        }

        public IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>> filter)
        {
            return _db.ShoppingCarts.Where(filter).Include(u => u.Product);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}