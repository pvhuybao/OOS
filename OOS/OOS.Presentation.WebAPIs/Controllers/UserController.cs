using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Products;
using OOS.Presentation.ApplicationLogic.Users.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUsersBusinessLogic _usersBusinessLogic;

        public UserController(IUsersBusinessLogic usersBusinessLogic)
        {
            _usersBusinessLogic = usersBusinessLogic;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]CreateUserRequest request)
        {            
            _usersBusinessLogic.CreateUser(request);
            return Ok();
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditUserRequest request)
        {
            _usersBusinessLogic.EditUser(request, id);
            return Ok();
        }

    }
}
