using System;
using System.Collections.Generic;
using System.Text;
using OOS.Domain.Orders.Models;
using OOS.Presentation.ApplicationLogic.Order;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public interface IOrderBusinessLogic
    {
        void DeleteOrder (string id);

        List<Orders> GetOders();

        Orders GetOdersById(string id);
    }
}
