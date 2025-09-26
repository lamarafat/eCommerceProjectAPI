using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model.Order;
using eCommerceProject.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Stripe.Checkout;



namespace eCommerceProject.BLL.Service.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public CheckoutService(ICartRepository cartRepository, IOrderRepository orderRepository, IEmailSender emailSender, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var subject = " ";
            var body = " ";
            var order = await _orderRepository.GetByOrderAsync(orderId);
            
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {
                order.Status = OrderStatus.Approved;
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItem>();
                var productUpdated = new List<(int productId, int quantity)>();
                foreach (var cartItem in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        TotalPrice = cartItem.Product.Price * cartItem.Count,
                        Price = cartItem.Product.Price,
                        Count = cartItem.Count
                    };
                    orderItems.Add(orderItem);
                    productUpdated.Add((cartItem.ProductId, cartItem.Count));
                }
                await _orderItemRepository.AddRangeAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await _productRepository.DecreaseQuantityAsync(productUpdated);

                subject = "Order Confirmation";
                body = $"<h1>Your order with ID {orderId} has been successfully placed</h1>" +
                    $"<p>Total Amount: ${order.TotalAmount}.</p>";
            }
            else if (order.PaymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "Order Confirmation";
                body = $"<h1>Your order with ID {orderId} has been successfully placed</h1>" +
                    $"<p>Total Amount: ${order.TotalAmount}.</p>";
            }
            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        

        public async Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            if (!cartItems.Any())
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "Your cart is empty."
                };
            }
            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = cartItems.Sum(ci => ci.Product.Price * ci.Count),
            };

             await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Order placed successfully with Cash on Delivery."
                };
            }
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/Checkouts/success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }

                var service = new Stripe.Checkout.SessionService();
                var session = service.Create(options);
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Redirecting to payment gateway.",
                    PaymentId = session.Id,
                    Url = session.Url
                };
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully.",
                    Url = session.Url
                };

            }
            return new CheckoutResponse
            {
                Success = false,
                Message = "Invalid payment method."
            };

        }

    }
}
