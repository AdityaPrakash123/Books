using Books.DataAccess.Data;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
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
        public ApplicationDbContext _db { get; set; }
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter)
        {
            return _db.ShoppingCarts.Where(filter).FirstOrDefault();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _db.ShoppingCarts.ToList();
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