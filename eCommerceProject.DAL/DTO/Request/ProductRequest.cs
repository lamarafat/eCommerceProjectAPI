using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Category;
using Microsoft.AspNetCore.Http;

namespace eCommerceProject.DAL.DTO.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int Discount { get; set; }
        public IFormFile ImageUrl { get; set; }
        public double Rating { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
