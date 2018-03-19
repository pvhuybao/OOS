using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Categories;
using OOS.Presentation.ApplicationLogic.Categories.Messages;
using OOS.Domain.Categories.Models;

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
        public IActionResult Get()
        {
            return Ok(_categoriesBusinessLogic.GetCategories());
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var request = new GetCategoryRequest();
            request.Id = id;

            return Ok(_categoriesBusinessLogic.GetCategory(request));
        }

        // GET: api/Category/{status}/status
        [HttpGet("{status}/status")]
        public IActionResult Get(Status status)
        {
            var cate = _categoriesBusinessLogic.Get(status);

            return Ok(cate);
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
