using AutoMapper;
using OOS.Infrastructure.Mongodb;
using System;
using OOS.Domain.Orders.Models;
using System.Collections.Generic;
using System.Text;
using OOS.Presentation.ApplicationLogic.Order.Messages;
using MongoDB.Driver;
using System.Linq;
using OOS.Infrastructure.Queries;

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

        public CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            var result = new CreateOrderResponse();
            var pro = _mapper.Map<CreateOrderRequest, Orders>(request);
            pro.Id = Guid.NewGuid().ToString();
            pro.Status = 0;
            _mongoDbRepository.Create(pro);
            result.Id = pro.Id;
            return result;
        }

        public void DeleteOrder(string id)
        {
            var orderToDelete = _mongoDbRepository.Get<Orders>(id);
            _mongoDbRepository.Delete(orderToDelete);
        }

        public EditOrderResponse EditOrder(string id, EditOrderRequest request)
        {
            var result = new EditOrderResponse();
            var ordDb = _mongoDbRepository.Get<Orders>(id);
            if (ordDb != null)
            {
                var ord = _mapper.Map<EditOrderRequest, Orders>(request);
                ord.Id = id;
                ord.CreatedDate = ordDb.CreatedDate;
                ord.UpdatedDate = DateTime.UtcNow;
                _mongoDbRepository.Replace(ord);
            }

            return result;
        }

        public PagedQueryResult<GetOrdersResponse> GetOders(GetOrdersRequest query)
        {
            var builder = Builders<Orders>.Filter;
            var filter = builder.Empty;

            if (!String.IsNullOrEmpty(query.Email))
            {
                filter = filter & builder.Where(it => it.Email.Equals(query.Email));
            }

            if (!String.IsNullOrEmpty(query.Phone))
            {
                filter = filter & builder.Where(it => it.Address.Any(a => a.Phone == query.Phone));
            }

            var orders = _mongoDbRepository.Find(filter);
            var totalItemCount = orders.Count();

            var ordersOverview = _mapper.Map<IEnumerable<GetOrdersResponse>>(orders
                .SortByDescending(it => it.CreatedDate)
                .Skip((query.Page - 1) * query.PageSize)
                .Limit(query.PageSize)
                .ToList());

            var pagedResult = new PagedQueryResult<GetOrdersResponse>(ordersOverview, totalItemCount, query.Page, query.PageSize);
            return pagedResult;
        }

        public Orders GetOdersById(string id)
        {
            return _mongoDbRepository.Get<Orders>(id);
        }

        public List<Orders> SearchOrders(string keyword)
        {
            var filter = Builders<Orders>.Filter.Where(p => p.Email.ToLower().Contains(keyword.ToLower()) || p.Address[0].Phone.Contains(keyword) || p.Address[1].Phone.Contains(keyword));
            var orders = _mongoDbRepository.Find(filter).ToList();
            return orders;
        }
    }
}
