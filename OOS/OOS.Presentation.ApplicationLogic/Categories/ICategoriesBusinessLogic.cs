using OOS.Domain.Categories.Models;
using OOS.Presentation.ApplicationLogic.Categories.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Categories
{
    public interface ICategoriesBusinessLogic
    {
        CreateCategoryResponse CreateCategory(CreateCategoryRequest request);

        List<GetCategoryResponse> GetCategories();

        GetCategoryResponse GetCategory(GetCategoryRequest request = null);

        List<Category> Get(Status status);

        void DeleteCategory(string id);

        EditCategoryResponse EditCategory(string id,EditCategoryRequest request);        
    }
}
