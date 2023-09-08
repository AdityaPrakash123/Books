using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(Expression<Func<ApplicationUser, bool>> filter);
        IEnumerable<ApplicationUser> GetAll();
    }
}
