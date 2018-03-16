using OOS.Domain.Orders.Models;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class EditOrderRequest
    {


        public string Email { get; set; }

        public string UserId { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public List<Address> Address { get; set; }

        public double Total { get; set; }

        public OrderStatus Status { get; set; }
    }
}
