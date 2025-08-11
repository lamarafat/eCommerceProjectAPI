using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using eCommerceProject.DAL.Model.Category;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using eCommerceProject.Model;
using eCommerceProject.Model.Category;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<CategoryAlt>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            {
                _context = context;
            }

        }

    }
}
