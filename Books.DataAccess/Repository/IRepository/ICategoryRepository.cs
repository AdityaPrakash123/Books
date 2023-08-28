﻿using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Update and Save
        void Update(Category category);
        void Save();
    }
}