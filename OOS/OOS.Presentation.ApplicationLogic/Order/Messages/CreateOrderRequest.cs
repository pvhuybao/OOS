using OOS.Domain.Orders.Models;
using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class CreateOrderRequest : RequestBase
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        public string UserId { get; set; }

        public string IdBill { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        [Required(ErrorMessage = "List Address is required")]
        public List<Address> Address { get; set; }

        [Required(ErrorMessage = "Total is required")]
        public double Total { get; set; }
    }
}
