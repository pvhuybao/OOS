using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order.Messages
{
    public class CreateOrderResponse : ResponseBase
    {
        public string Id { get; set; }
    }
}
