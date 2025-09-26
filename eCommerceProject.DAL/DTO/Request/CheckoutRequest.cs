using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Order;

namespace eCommerceProject.DAL.DTO.Request
{
    public class CheckoutRequest
    {
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
