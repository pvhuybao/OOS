using AutoMapper;
using OOS.Infrastructure.Mongodb;
using System;
using OOS.Domain.Orders.Models;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Order
{
    public class OrderBusinessLogic : IOrderBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public OrderBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }
        public void DeleteOrder(string id)
        {
            var orderToDelete = _mongoDbRepository.Get<Orders>(id);
            _mongoDbRepository.Delete(orderToDelete);
        }
    }
}
