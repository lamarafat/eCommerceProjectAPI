using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Order;

namespace eCommerceProject.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task <Order> GetByOrderAsync(int orderId);
        Task <Order> AddAsync (Order order);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<List<Order>> GetByUsersAsync(string userId);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus);
        Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId);
    }
}
