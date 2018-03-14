using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products
{
    public interface IProductsBusinessLogic
    {
        CreateProductResponse CreateProduct(CreateProductRequest request);
        void DeleteProduct(string id);
    }
}
