using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.Model.Order
{
    public enum OrderStatus
    {
        Pending=1,
        Processing=2,
        Shipped=3,
        Delivered=4,
        Cancelled=5,
        Approved = 6
    }
    public enum PaymentMethodEnum
    {
        Cash= 1,
        Visa=2
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentMethodEnum PaymentMethod  { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentId { get; set; }
        public string UserId { get; set; }
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
