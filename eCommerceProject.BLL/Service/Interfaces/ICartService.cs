using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface ICartService
    {
       Task <bool> AddToCartAsync(CartRequest request, string userId);
       Task <CartSummaryResponse> CartSummaryResponseAsync(string userId);
       Task <bool> ClearCartAsync(string userId);
    }
}
