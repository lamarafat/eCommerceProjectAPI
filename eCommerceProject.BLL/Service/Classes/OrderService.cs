using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Model.Order;
using eCommerceProject.DAL.Repositories.Interfaces;

namespace eCommerceProject.BLL.Service.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, newStatus);
        }

        public async Task<Order> GetByOrderAsync(int orderId)
        {
            return await _orderRepository.GetByOrderAsync(orderId);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<List<Order>> GetByUsersAsync(string userId)
        {
            return await _orderRepository.GetByUsersAsync(userId);
        }
    }
}
