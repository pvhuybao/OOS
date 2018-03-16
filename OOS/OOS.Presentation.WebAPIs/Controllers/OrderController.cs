using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Order;
using OOS.Presentation.ApplicationLogic.Order.Messages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderBusinessLogic _orderBusinessLogic;

        public OrderController(IOrderBusinessLogic orderBusinessLogic)
        {
            _orderBusinessLogic = orderBusinessLogic;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var listOrders = _orderBusinessLogic.GetOders();
            return Ok(listOrders);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var order = _orderBusinessLogic.GetOdersById(id);
            return Ok(order);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderRequest request)
        {
            var result = _orderBusinessLogic.CreateOrder(request);
            return Ok(result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditOrderRequest value)
        {
            
            _orderBusinessLogic.EditOrder(id,value);
            return Ok();

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _orderBusinessLogic.DeleteOrder(id);
            return Ok();
        }
    }
}
