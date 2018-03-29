using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Products.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OOS.Domain.Categories.Models;

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

        public EditProductResponse EditProduct(string id, EditProductResquest request)
        {
            var pro = _mapper.Map<EditProductResquest, Product>(request);
            pro.Id = id;

            _mongoDbRepository.Replace<Product>(pro);

            var result = _mapper.Map<Product, EditProductResponse>(pro);
            return result;
            
        }

        public void DeleteProduct(string id)
        {
            var product = _mongoDbRepository.Get<Product>(id);
            _mongoDbRepository.Delete(product);
        }
        public List<GetProductExtraCategoryNameResponse> GetProduct()
        {
            var filter = Builders<Product>.Filter.Empty;
            var listProducts = _mongoDbRepository.Find(filter).ToList();
            List<GetProductExtraCategoryNameResponse> listResult = new List<GetProductExtraCategoryNameResponse>();
            foreach(var p in listProducts)
            {
                var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(p);
                //add category name
                //response.CategoryName = _mongoDbRepository.Get<Category>(response.IdCategory).Name;
                response.CalculateProductValues();
                listResult.Add(response);
            }
            return listResult;     
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

        public List<Product> ProductWidget(string widget)
        {
            List<Product> products = new List<Product>();
            if (widget == "newestProduct")
            {
                //TimeSpan timeSpan = TimeSpan.FromDays(10);
                var filter = Builders<Product>.Filter.Empty;
                products.AddRange(_mongoDbRepository.Find(filter).ToList().OrderByDescending(t => t.CreatedDate).Take(8));
            }else if(widget == "topSales")
            {
                //wait for further update
            }
            else if (widget == "topDiscount")
            {
                //wait for further update
            }
            return products;
        }

        public List<Product> GetProductsBaseOnIDCategory(string idCategory)
        {
            var filter = Builders<Product>.Filter.Where(p => p.IdCategory.Equals(idCategory));
            var products = _mongoDbRepository.Find(filter).ToList();
            return products;
        }

        public List<Product> SearchProduct(string keyword)
        {
            var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
            var products = _mongoDbRepository.Find(filter).ToList();
            return products;
        }

        public List<Product> SearchProductByIdCategory(string idCategory, string keyword)
        {
            var products = new List<Product>();
            if (idCategory == "all") {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
                products = _mongoDbRepository.Find(filter).ToList();
            }
            else
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.IdCategory.Equals(idCategory));
                products = _mongoDbRepository.Find(filter).ToList();
            }
            return products;
        }
    }
}