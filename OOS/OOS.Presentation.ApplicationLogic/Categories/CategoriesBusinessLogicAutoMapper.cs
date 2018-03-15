using AutoMapper;
using OOS.Domain.Categories.Models;
using OOS.Presentation.ApplicationLogic.Categories.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Categories
{
    public class CategoriesBusinessLogicAutoMapper : Profile
    {
        public CategoriesBusinessLogicAutoMapper()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<EditCategoryRequest, Category>();
            CreateMap<GetCategoryRequest, Category>();
            CreateMap<Category, GetCategoryResponse>();
            CreateMap<Category, EditCategoryResponse>();
            CreateMap<Category, CreateCategoryResponse>();
        }
    }
}
