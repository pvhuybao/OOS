using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Orders.Models
{
    public class OrderDetails 
    {
        public string IdProduct { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }
    }
}
