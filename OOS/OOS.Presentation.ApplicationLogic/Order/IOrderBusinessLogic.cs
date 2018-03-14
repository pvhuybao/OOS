using System;
using System.Collections.Generic;
using System.Text;
using OOS.Domain.Orders.Models;
using OOS.Presentation.ApplicationLogic.Order;
using OOS.Presentation.ApplicationLogic.Order.Messages;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public interface IOrderBusinessLogic
    {

        void DeleteOrder(string id);
        EditOrderResponse EditOrder(EditOrderRequest request);

        void DeleteOrder (string id);

        List<Orders> GetOders();

        Orders GetOdersById(string id);

    }
}
