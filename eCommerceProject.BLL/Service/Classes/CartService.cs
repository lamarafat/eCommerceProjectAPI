using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model.Cart;
using eCommerceProject.DAL.Repositories.Classes;
using eCommerceProject.DAL.Repositories.Interfaces;

namespace eCommerceProject.BLL.Service.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCartAsync(CartRequest request, string UserId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = UserId,
                Count = 1
            };
            return await _cartRepository.AddAsync(newItem) > 0;
        }

        public async Task <CartSummaryResponse> CartSummaryResponseAsync(string UserId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            var response = new CartSummaryResponse
            {
                Carts = cartItems.Select(ci => new CartResponse
                {
                    ProductId = ci.ProductId,
                    Count = ci.Count,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price
                }).ToList()
            };
            return response;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.ClearCartAsync(userId);
        }
    }
}
