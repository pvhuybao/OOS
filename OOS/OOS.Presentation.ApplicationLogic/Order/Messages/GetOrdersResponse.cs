using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class GetOrdersResponse
    {
        public string Id { get; set; }

        public string IdBill { get; set; }
        
        public string Email { get; set; }
        
        public string UserId { get; set; }                

        public double Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderStatus Status { get; set; }
    }
}
