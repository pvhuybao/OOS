using AutoMapper;
using OOS.Domain.Orders.Models;
using OOS.Presentation.ApplicationLogic.Order.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public class OrderBusinessLogicAutoMapper: Profile
    {
        public OrderBusinessLogicAutoMapper()
        {
            CreateMap<EditOrderRequest, Orders>();
        }
    }
}
