using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.Model;
using eCommerceProject.DAL.Model.Products;

namespace eCommerceProject.DAL.Model.Brand
{
    public class BrandAlt : BaseModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
