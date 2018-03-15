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
    
        void DeleteCategory(string id);
    }
}
