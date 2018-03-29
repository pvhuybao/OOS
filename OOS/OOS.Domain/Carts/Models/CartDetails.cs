using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Carts.Models
{
    public class CartDetails
    {
        public string IdProduct { get; set; }

        public string Code { get; set; }

        public string NameProduct { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }        
    }
}
