using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Products.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


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
        public List<Product> GetProduct()
        {
            var filter = Builders<Product>.Filter.Empty;
            var listProducts = _mongoDbRepository.Find(filter).ToList();
            return listProducts;
        }
        public Product GetProduct(string id)
        {
            return _mongoDbRepository.Get<Product>(id);
        }

        public bool checkExistedCode(string code)
        {
            var filter = Builders<Product>.Filter.Where(p => p.Code == code);
            var count = _mongoDbRepository.Find(filter).Count();
            if (count > 0)
                return true;
            return false;
        }
        public List<Product> SearchProduct(string keyword)
        {
            var filter = Builders<Product>.Filter.Where(p => p.Name.Contains(keyword));
            var products = _mongoDbRepository.Find(filter).ToList();
            return products;
        }
        public List<Product> GetProductsBaseOnIDCategory(string idCategory)
        {
            var filter = Builders<Product>.Filter.Where(p => p.IdCategory.Equals(idCategory));
            var products = _mongoDbRepository.Find(filter).ToList();
            return products;
        }
    }
}