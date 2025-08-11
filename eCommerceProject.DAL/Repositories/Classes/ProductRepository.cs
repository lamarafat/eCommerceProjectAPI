using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Products;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            {
                _context = context;
            }

        }

        
    }
}
