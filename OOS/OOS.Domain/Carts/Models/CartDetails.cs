using OOS.Domain.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Carts.Models
{
    public class CartDetails
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
