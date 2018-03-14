using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Orders.Models
{
    public class Address
    {
        public string Name { get; set;}

        public string Phone { get; set; }

        public string Street { get; set; }

        public string District { get; set; }

        public string Province { get; set; }

        public bool TypeAddress { get; set; }
    }
}
