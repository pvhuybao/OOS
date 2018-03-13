using AutoMapper;
using OOS.Domain.Products.Models;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products
{
    public class ProductsBusinessLogicAutoMapper : Profile
    {
        public ProductsBusinessLogicAutoMapper()
        {
            CreateMap<CreateProductRequest, Product>();            
        }
    }
}
