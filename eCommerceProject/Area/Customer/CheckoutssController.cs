using System.Security.Claims;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CheckoutssController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutssController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody]CheckoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkoutService.ProcessPaymentAsync(request, userId, Request);
            return Ok(response);
        }
        [HttpGet("Success/{orderId}")]
        [AllowAnonymous]
        public IActionResult Success([FromRoute] int orderId)
        {
            var result = _checkoutService.HandlePaymentSuccessAsync(orderId);

            return Ok(result);
        }
    }
}
