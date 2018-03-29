using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Carts.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Carts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Carts
{
    public class CartBusinessLogic : ICartBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public CartBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public CreateCartResponse CreateCart(CreateCartRequest request)
        {
            var cart = _mapper.Map<CreateCartRequest, Cart>(request);
            cart.Id = Guid.NewGuid().ToString();

            _mongoDbRepository.Create<Cart>(cart);

            var result = _mapper.Map<Cart, CreateCartResponse>(cart);
            return result;
        }

        public List<Cart> GetCart()
        {
            var filter = Builders<Cart>.Filter.Empty;
            var listCart = _mongoDbRepository.Find(filter).ToList();
            return listCart;
        }

        public Cart GetCartById(string id)
        {
            var cart = _mongoDbRepository.Get<Cart>(id);
            return cart;
        }

        public void DeleteCart(string id)
        {
            var cart = _mongoDbRepository.Get<Cart>(id);
            _mongoDbRepository.Delete(cart);
        }

        public EditCartResponse EditCart(string id, EditCartRequest request)
        {
            var cart = _mapper.Map<EditCartRequest, Cart>(request);
            cart.Id = id;

            _mongoDbRepository.Replace<Cart>(cart);

            var result = _mapper.Map<Cart, EditCartResponse>(cart);
            return result;
        }
    }
}
