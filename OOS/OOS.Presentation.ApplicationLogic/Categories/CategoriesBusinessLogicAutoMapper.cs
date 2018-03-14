using AutoMapper;
using OOS.Domain.Categories.Models;
using OOS.Presentation.ApplicationLogic.Categories.Messages;

namespace OOS.Presentation.ApplicationLogic.Categories
{
    public class CategoriesBusinessLogicAutoMapper : Profile
    {
        public CategoriesBusinessLogicAutoMapper()
        {
            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}
