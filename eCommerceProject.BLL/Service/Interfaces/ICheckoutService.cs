using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface ICheckoutService
    {
        Task <CheckoutResponse> ProcessPaymentAsync (CheckoutRequest request, string UserId, HttpRequest httpRequest);
        Task <bool> HandlePaymentSuccessAsync (int orderId);
    }
}
