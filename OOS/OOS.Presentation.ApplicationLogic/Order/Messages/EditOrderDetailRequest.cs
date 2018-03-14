using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class EditOrderDetailRequest
    {
        public string IdProduct { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }
    }
}
