﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Model.Order
{
    [PrimaryKey (nameof(OrderId), nameof(ProductId))]
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Price { get; set; }
        public int  Count { get; set; }
    }
}
