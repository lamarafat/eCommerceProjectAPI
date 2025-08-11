using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.Model;
using eCommerceProject.DAL.Model.Category;
using eCommerceProject.DAL.Model.Brand;

namespace eCommerceProject.DAL.Model.Products
{
    public class Product : BaseModel
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public  int Discount { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public int CategoryId { get; set; }
        public CategoryAlt Category { get; set; }
        public int? BrandId { get; set; }
        public BrandAlt? Brand { get; set; }


    }
}
