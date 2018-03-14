using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Categories;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/Category")]
    public class CategoryController : Controller
    {

        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;

        public CategoryController(ICategoriesBusinessLogic categoriesBusinessLogic)
        {
            _categoriesBusinessLogic = categoriesBusinessLogic;
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _categoriesBusinessLogic.DeleteCategory(id);
            return Ok();
        }
    }
}