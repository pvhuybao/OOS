using AutoMapper;
using OOS.Domain.Products.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products
{
    public class ProductsBusinessLogic : IProductsBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public ProductsBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            var result = new CreateProductResponse();
            var pro = _mapper.Map<CreateProductRequest, Product>(request);
            pro.Id = Guid.NewGuid().ToString();
            _mongoDbRepository.Create(pro);
            return result;
        }

        public CreateProductResponse EditProduct(CreateProductRequest request, string id)
        {
            var result = new CreateProductResponse();
            var pro = _mapper.Map<CreateProductRequest, Product>(request);         
                pro.Id = id;    
            _mongoDbRepository.Replace<Product>(pro);
            return result;
        }

        public void DeleteProduct(string id)
        {
            var product = _mongoDbRepository.Get<Product>(id);
            _mongoDbRepository.Delete(product);
        }
    }
}