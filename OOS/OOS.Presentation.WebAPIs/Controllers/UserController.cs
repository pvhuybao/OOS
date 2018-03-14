using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Users;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUsersBusinessLogic _UsersBusinessLogic;

        public UserController(IUsersBusinessLogic UsersBusinessLogic)
        {
            _UsersBusinessLogic = UsersBusinessLogic;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var listUser = _UsersBusinessLogic.GetUser();
            return Ok(listUser);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {   
            var user = _UsersBusinessLogic.GetUser(id);
            return Ok(user);
        }
    }
}
