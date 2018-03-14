using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OOS.Presentation.ApplicationLogic.Categories;
using OOS.Presentation.ApplicationLogic.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Configs
{
    public class AutoMapperConfig
    {
        public static void Configure(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductsBusinessLogicAutoMapper>();
                c.AddProfile<CategoriesBusinessLogicAutoMapper>();
            });

            services.AddAutoMapper(n => config.CreateMapper());
        }
    }
}