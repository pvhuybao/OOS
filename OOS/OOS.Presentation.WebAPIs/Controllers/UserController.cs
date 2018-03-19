using System.Collections.Generic;
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
      
        // GET: api/User/5
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
            var rs = _usersBusinessLogic.CreateUser(request);
            return Ok(rs);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _usersBusinessLogic.DeleteUser(id);
            return Ok();
        }
            
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditUserRequest request)
        {
            var rs = _usersBusinessLogic.EditUser(request, id);
            return Ok(rs);
        }

    }
}
