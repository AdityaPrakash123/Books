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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ApplicationUser Get(Expression<Func<ApplicationUser, bool>> filter)
        {
            return _db.ApplicationUsers.Where(filter).FirstOrDefault();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            IQueryable<ApplicationUser> applicationUsers = _db.ApplicationUsers;
            return applicationUsers.ToList();
        }        
    }
}
