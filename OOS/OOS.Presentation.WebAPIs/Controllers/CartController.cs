using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Carts;
using OOS.Presentation.ApplicationLogic.Carts.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Produces("application/json")]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        private readonly ICartBusinessLogic _cartsBusinessLogic;

        public CartController(ICartBusinessLogic cartsBusinessLogic)
        {
            _cartsBusinessLogic = cartsBusinessLogic;
        }

        // GET: api/Cart
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cartsBusinessLogic.GetCart());
        }

        // GET: api/Cart/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var cart = _cartsBusinessLogic.GetCartById(id);
            return Ok(cart);
        }

        [HttpGet]
        [Route("{email}/email")]
        public IActionResult GetCartBaseOnEmail(string email)
        {
            var cart = _cartsBusinessLogic.GetCartBaseOnEmail(email);
            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public IActionResult Post([FromBody]CreateCartRequest value)
        {
            var rs = _cartsBusinessLogic.CreateCart(value);
            return Ok(rs);
        }

        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditCartRequest value)
        {
            var rs = _cartsBusinessLogic.EditCart(id, value);
            return Ok(rs);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _cartsBusinessLogic.DeleteCart(id);
            return Ok();
        }
    }
}
