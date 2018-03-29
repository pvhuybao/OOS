using System;
using System.Collections.Generic;
using System.Text;
using OOS.Domain.Orders.Models;
using OOS.Presentation.ApplicationLogic.Order;
using OOS.Presentation.ApplicationLogic.Order.Messages;
using OOS.Infrastructure.Queries;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public interface IOrderBusinessLogic
    {
        CreateOrderResponse CreateOrder(CreateOrderRequest request);
        void DeleteOrder (string id);

        EditOrderResponse EditOrder(string id,EditOrderRequest request);

        PagedQueryResult<GetOrdersResponse> GetOders(GetOrdersRequest query);

        Orders GetOdersById(string id);

        List<Orders> SearchOrders(string keyword);
    }
}
