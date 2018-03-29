using OOS.Domain.Orders.Models;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class EditOrderRequest
    {
        public string IdBill { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        public string UserId { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        [Required(ErrorMessage = "List Addrss is required")]
        public List<Address> Address { get; set; }

        [Required(ErrorMessage = "Total is required")]
        public double Total { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public OrderStatus Status { get; set; }
    }
}
