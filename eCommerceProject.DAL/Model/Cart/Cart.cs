using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Model.Cart
{
    [PrimaryKey(nameof(ProductId), nameof(UserId))]
    public class Cart
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
