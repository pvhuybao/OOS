using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Users;
using OOS.Presentation.ApplicationLogic.Users.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUsersBusinessLogic _usersBusinessLogic;

        public UserController(IUsersBusinessLogic UsersBusinessLogic)
        {
            _usersBusinessLogic = UsersBusinessLogic;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var listUser = _usersBusinessLogic.GetUser();
            return Ok(listUser);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = _usersBusinessLogic.GetUser(id);
            return Ok(user);
        }
     
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]CreateUserRequest request)
        {            
            _usersBusinessLogic.CreateUser(request);
            return Ok();
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
