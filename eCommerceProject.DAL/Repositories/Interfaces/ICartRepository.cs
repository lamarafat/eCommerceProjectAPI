using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Cart;

namespace eCommerceProject.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task <int> AddAsync(Cart cart);
        Task <List<Cart>> GetUserCartAsync(string UserId);
        Task<bool> ClearCartAsync(string userId);

    }
}
