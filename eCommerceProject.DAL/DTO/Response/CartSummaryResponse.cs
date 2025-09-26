using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.DTO.Response
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Carts { get; set; } = new List<CartResponse>();
        public decimal CountTotal => Carts.Sum(i => i.TotalPrice);
    }
}
