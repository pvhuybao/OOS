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
using OOS.Domain.Orders.Models;
using System.Dynamic;
using System.Reflection;

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
            foreach (var p in listProducts)
            {
                var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(p);
                //add category name
                response.CategoryName = _mongoDbRepository.Get<Category>(response.IdCategory).Name;
                //calculate other values of Product:min-max price, total quantity, basic image
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

        public List<GetProductExtraCategoryNameResponse> ProductWidget(string widget)
        {
            List<Product> products = new List<Product>();
            List<GetProductExtraCategoryNameResponse> listResult = new List<GetProductExtraCategoryNameResponse>();
            if (widget == "newestProduct")
            {
                //TimeSpan timeSpan = TimeSpan.FromDays(10);
                var filter = Builders<Product>.Filter.Empty;
                products.AddRange(_mongoDbRepository.Find(filter).ToList().OrderByDescending(t => t.CreatedDate).Take(8));
                foreach (var product in products)
                {
                    var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(product);
                    //calculate other values of Product:min-max price, total quantity, basic image
                    response.CalculateProductValues();
                    listResult.Add(response);
                }
            }
            else if (widget == "topSales")
            {
                var orderfilter = Builders<Orders>.Filter.Empty;
                var listOrders = _mongoDbRepository.Find(orderfilter).ToList();
                var listDetails = new List<OrderDetails>();

                foreach (var order in listOrders)
                {
                    listDetails.AddRange(order.OrderDetails);
                }

                var groupDetails = listDetails
                    .GroupBy(g => g.IdProduct)
                    .Select(d => new
                    {
                        id = d.Key,
                        quantity = d.Sum(s => s.Quantity)
                    })
                    .OrderByDescending(o => o.quantity).ToList();

                var prolist = groupDetails.Select(d => d.id).ToList();

                var categoryfilter = Builders<Category>.Filter.Where(p => p.Status.Equals(Shared.Enums.CategoryStatus.Publish));
                var categories = _mongoDbRepository.Find(categoryfilter).ToList();
                var publishcat = categories.Select(d => d.Id).ToList();
                
                var filter = Builders<Product>.Filter.Where(p => prolist.Contains(p.Id) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory));
                products = _mongoDbRepository.Find(filter).ToList();

                var listProducts = products.Join(groupDetails, a => a.Id, b => b.id, (a, b) => (a,b)).OrderByDescending(o => o.b.quantity).Take(8).ToList();
                foreach (var product in listProducts)
                {
                    var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(product.a);
                    //calculate other values of Product:min-max price, total quantity, basic image
                    response.CalculateProductValues();
                    listResult.Add(response);
                }
            }
            else if (widget == "topDiscount")
            {
                //wait for further update
                products.AddRange(_mongoDbRepository.Find<Product>().SortByDescending(p => p.Discount).Limit(8).ToList());
                
                foreach (var product in products)
                {
                    var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(product);
                    //calculate other values of Product:min-max price, total quantity, basic image
                    response.CalculateProductValues();
                    listResult.Add(response);
                }
            }
            return listResult;
        }

        public List<GetProductExtraCategoryNameResponse> GetProductsBaseOnIDCategory(string idCategory)
        {
            var filter = Builders<Product>.Filter.Where(p => p.IdCategory.Equals(idCategory));
            var listProducts = _mongoDbRepository.Find(filter).ToList();
            List<GetProductExtraCategoryNameResponse> listResult = new List<GetProductExtraCategoryNameResponse>();
            foreach (var p in listProducts)
            {
                var response = _mapper.Map<Product, GetProductExtraCategoryNameResponse>(p);
                //add category name
                response.CategoryName = _mongoDbRepository.Get<Category>(response.IdCategory).Name;
                //calculate other values of Product:min-max price, total quantity, basic image
                response.CalculateProductValues();
                listResult.Add(response);
            }
            return listResult;

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

            var categoryfilter = Builders<Category>.Filter.Where(p => p.Status.Equals(Shared.Enums.CategoryStatus.Publish));
            var categories = _mongoDbRepository.Find(categoryfilter).ToList();
            var publishcat = categories.Select(d => d.Id).ToList();


            if (idCategory == "all")
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory));
                products = _mongoDbRepository.Find(filter).ToList();
            }
            else
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory) && p.IdCategory.Equals(idCategory));
                products = _mongoDbRepository.Find(filter).ToList();
            }

            return products;
        }
    }
}