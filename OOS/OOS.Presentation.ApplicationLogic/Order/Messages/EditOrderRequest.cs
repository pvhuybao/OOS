using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
     public class EditOrderRequest
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public List<EditOrderDetailRequest> OrderDetails { get; set; }

        public List<EditAddressRequest> Address { get; set; }

        public double Total { get; set; }
    }
}
