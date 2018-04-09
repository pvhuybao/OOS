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
using OOS.Infrastructure.Queries;

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
        public PagedQueryResult<GetProductExtraCategoryNameResponse> GetProduct(GetProductsRequest query)
        {
            var filter = Builders<Product>.Filter.Empty;
            var listProducts = _mongoDbRepository.Find(filter);           
            var totalItemCount = listProducts.Count();

            var ordersOverview = _mapper.Map<IEnumerable<GetProductExtraCategoryNameResponse>>(listProducts
                .SortByDescending(it => it.CreatedDate)
                .Skip((query.Page - 1) * query.PageSize)
                .Limit(query.PageSize)
                .ToList()).ToList();

            var listCat = _mongoDbRepository.Find(Builders<Category>.Filter.Empty).ToList();
            for (int i = 0; i < ordersOverview.Count(); i++)
            {
                ordersOverview[i].CategoryName = listCat.SingleOrDefault(n => n.Id == ordersOverview[i].IdCategory).Name;
                ordersOverview[i].CalculateProductValues();
            }            

            var pagedResult = new PagedQueryResult<GetProductExtraCategoryNameResponse>(ordersOverview, totalItemCount, query.Page, query.PageSize);
            return pagedResult;
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
                var orders = _mongoDbRepository.Find<Orders>().ToList();
                var details = new List<OrderDetails>();
                foreach (var order in orders)
                {
                    details.AddRange(order.OrderDetails);
                }

                var groupDetails = details
                    .GroupBy(g => g.IdProduct)
                    .Select(d => new
                    {
                        id = d.Key,
                        quantity = d.Sum(s => s.Quantity)
                    })
                    .ToList();

                var categoryfilter = Builders<Category>.Filter.Where(p => p.Status.Equals(Shared.Enums.CategoryStatus.Publish));
                var categories = _mongoDbRepository.Find(categoryfilter).ToList();
                var publishcat = categories.Select(d => d.Id).ToList();

                var productfilter = Builders<Product>.Filter.Where(p => p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory));
                products = _mongoDbRepository.Find(productfilter).ToList();

                var sortlist = products.Join(groupDetails, a => a.Id, b => b.id, (a, b) => (a, b)).ToList().OrderByDescending(o => o.b.quantity).Take(8);

                foreach (var product in sortlist)
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

        public List<Product> SearchProduct(string idCategory, string keyword)
        {
            var products = new List<Product>();

            var categoryfilter = Builders<Category>.Filter.Where(p => p.Status.Equals(Shared.Enums.CategoryStatus.Publish));
            var categories = _mongoDbRepository.Find(categoryfilter).ToList();
            var publishcat = categories.Select(d => d.Id).ToList();

            if (idCategory == "all")
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory));
                products = _mongoDbRepository.Find(filter).ToList().ToList();

            }
            else
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory) && p.IdCategory.Equals(idCategory));
                products = _mongoDbRepository.Find(filter).ToList().ToList();
            }

            products = products.Take(10).ToList();

            return products;
        }

        public PagedQueryResult<GetProductExtraCategoryNameResponse> SearchProductByIdCategory(GetProductsRequest query)
        {
            var products = new List<Product>();

            var categoryfilter = Builders<Category>.Filter.Where(p => p.Status.Equals(Shared.Enums.CategoryStatus.Publish));
            var categories = _mongoDbRepository.Find(categoryfilter).ToList();
            var publishcat = categories.Select(d => d.Id).ToList();

            if (query.IdCategory == "all")
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(query.Keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory));
                products = _mongoDbRepository.Find(filter).ToList().ToList();
                
            }
            else
            {
                var filter = Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(query.Keyword.ToLower()) && p.Status.Equals(Shared.Enums.ProductStatus.Publish) && publishcat.Contains(p.IdCategory) && p.IdCategory.Equals(query.IdCategory));
                products = _mongoDbRepository.Find(filter).ToList().ToList();
            }

            var ordersOverview = _mapper.Map<IEnumerable<GetProductExtraCategoryNameResponse>>(products).ToList();

            var listCat = _mongoDbRepository.Find(Builders<Category>.Filter.Empty).ToList();
            for (int i = 0; i < ordersOverview.Count(); i++)
            {
                ordersOverview[i].CategoryName = listCat.SingleOrDefault(n => n.Id == ordersOverview[i].IdCategory).Name;
                ordersOverview[i].CalculateProductValues();
            }

            ordersOverview = ordersOverview.Where(p => (p.MinPrice >= query.MinInPrice) && (p.MinPrice <= query.MaxInPrice)).ToList();

            if (query.Sort == "price") ordersOverview = ordersOverview.OrderBy(o => o.MinPrice).ToList();
            else ordersOverview = ordersOverview.OrderBy(o => o.Name).ToList();

            var totalItemCount = ordersOverview.Count();

            ordersOverview = ordersOverview
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var pagedResult = new PagedQueryResult<GetProductExtraCategoryNameResponse>(ordersOverview, totalItemCount, query.Page, query.PageSize);
            return pagedResult;
        }
    }
}