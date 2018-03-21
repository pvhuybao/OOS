using OOS.Domain.Products.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products
{
    public interface IProductsBusinessLogic
    {
        CreateProductResponse CreateProduct(CreateProductRequest request);

        EditProductResponse EditProduct(string Id, EditProductResquest Request);

        void DeleteProduct(string id);

        List<Product> GetProduct();

        Product GetProduct(string id);

        bool checkExistedCode(string code);
        
        List<Product> GetProductsBaseOnIDCategory(string idCategory);

        List<Product> SearchProduct(string keyword);

        List<Product> SearchProductByIdCategory(string idCategory, string keyword);

        List<Product> ProductWidget(string widget);

    }
}
