using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using eCommerceProject.DAL.Model.Brand;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class BrandRepository : GenericRepository<BrandAlt>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            {
                _context = context;
            }

        }

    }
}
