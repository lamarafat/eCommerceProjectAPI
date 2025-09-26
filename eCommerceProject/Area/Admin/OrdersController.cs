using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Model.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(OrderStatus status)
        {
  
            var orders = await _orderService.GetByStatusAsync(status);
            return Ok(orders);
        }
        [HttpPatch("changeStatus/{orderId}")]
        public async Task<IActionResult> ChangeStatus(int orderId,[FromBody] OrderStatus newStatus)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, newStatus);
            return Ok(new { message = "status is changed"});
        }

    }
}
