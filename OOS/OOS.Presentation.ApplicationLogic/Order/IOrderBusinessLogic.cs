using System;
using System.Collections.Generic;
using System.Text;
using OOS.Presentation.ApplicationLogic.Order.Messages;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public interface IOrderBusinessLogic
    {
        void DeleteOrder (string id);
    }
}
