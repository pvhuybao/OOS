using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Carts.Models
{
    public class CartProduct
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public int Price { get; set; }

        public String Description { get; set; }

        public String Image { get; set; }

        public string IdCategory { get; set; }
    }
}
