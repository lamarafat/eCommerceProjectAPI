using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Order;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetByOrderAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task<List<Order>> GetAllWithUserAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders.Where(o=> o.Status == status).OrderByDescending(O=>O.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetByUsersAsync(string userId)
        {
            return await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<bool> ChangeStatusAsync (int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order is null) return false;
            order.Status = newStatus;
            var result = await _context.SaveChangesAsync();
            return result >0;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .AnyAsync(o => o.UserId == userId && o.Status == OrderStatus.Approved && o.OrderItems
                .Any(oi => oi.ProductId == productId));
        }
    }
}
