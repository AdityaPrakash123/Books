using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Update and Save
        Category Get(Expression<Func<Category, bool>> filter);
        IEnumerable<Category> GetAll();
        void Update(Category category);
        void Save();
    }
}
