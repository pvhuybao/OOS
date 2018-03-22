using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Shared.Enums
{
    public enum  OrderStatus
    {
        //chưa xác nhận
        Confirming = 0,

        //đã xác nhận
        Confirmed = 1,

        //đang giao hàng
        Shipping = 2,

        //đã giao hàng
        Shipped = 3
    }
}
