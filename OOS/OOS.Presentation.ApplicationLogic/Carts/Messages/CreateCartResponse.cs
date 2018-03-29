using OOS.Domain.Carts.Models;
using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Carts.Messages
{
    public class CreateCartResponse : ResponseBase
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public List<CartDetails> CartDetails { get; set; }

        public double Total { get; set; }
    }
}
