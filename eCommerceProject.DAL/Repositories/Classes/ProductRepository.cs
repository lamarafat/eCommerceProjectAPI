using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Products;
using Microsoft.EntityFrameworkCore;

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

        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if (product.StockQuantity < item.quantity)
                {
                    throw new Exception("no enough stock quantity");
                }
                product.StockQuantity -= item.quantity;
            }
            await _context.SaveChangesAsync();

        }
        public async Task<List<Product>> GetAllProductWithImage()
        {
            return await _context.Products.Include(p => p.SubImages).Include(p => p.Reviews).ThenInclude(r => r.User).ToListAsync();
        }
    }
}
