using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Products;
using OOS.Presentation.ApplicationLogic.Products.Messages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductsBusinessLogic _productsBusinessLogic;

        public ProductController(IProductsBusinessLogic productsBusinessLogic)
        {
            _productsBusinessLogic = productsBusinessLogic;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateProductRequest request)
        {
            _productsBusinessLogic.CreateProduct(request);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]CreateProductRequest request)
        {
            _productsBusinessLogic.EditProduct(request, id);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}