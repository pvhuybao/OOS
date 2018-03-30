using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Carts.Models
{
    public class CartDetails
    {
        public CartProduct Product { get; set; }

        public int Quantity { get; set; }
    }
}
