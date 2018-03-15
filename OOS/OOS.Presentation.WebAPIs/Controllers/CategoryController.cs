using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Categories;
using OOS.Presentation.ApplicationLogic.Categories.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;

        public CategoryController(ICategoriesBusinessLogic categoriesBusinessLogic)
        {
            _categoriesBusinessLogic = categoriesBusinessLogic;
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody]CreateCategoryRequest value)
        {
            var rs = _categoriesBusinessLogic.CreateCategory(value);
            return Ok(rs);
        }
        
        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditCategoryRequest value)
        {
            var rs = _categoriesBusinessLogic.EditCategory(id,value);
            return Ok(rs);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _categoriesBusinessLogic.DeleteCategory(id);
            return Ok();
        }
    }
}
