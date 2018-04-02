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

         List<GetProductExtraCategoryNameResponse> GetProduct();

        Product GetProduct(string id);

        bool checkExistedCode(string code);
        
        List<GetProductExtraCategoryNameResponse> GetProductsBaseOnIDCategory(string idCategory);

        List<Product> SearchProduct(string keyword);

        List<Product> SearchProductByIdCategory(string idCategory, string keyword);

        List<GetProductExtraCategoryNameResponse> ProductWidget(string widget);

    }
}
