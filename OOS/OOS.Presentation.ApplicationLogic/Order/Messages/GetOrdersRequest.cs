using OOS.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class GetOrdersRequest : ListQueryBase
    {
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
