using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.Model;
using eCommerceProject.DAL.Model.Products;

namespace eCommerceProject.DAL.Model.Category
{
    public class CategoryAlt : BaseModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
