using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Users;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using OOS.Domain.Users.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUsersBusinessLogic _usersBusinessLogic;
        private readonly IConfiguration _configuration;

        public UserController(IUsersBusinessLogic UsersBusinessLogic, IUserService userService, IConfiguration configuration)
        {
            _usersBusinessLogic = UsersBusinessLogic;
            _userService = userService;
            _configuration = configuration;
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

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody]CreateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.FindByEmailAsync(request.Email).Result;
                if (user != null)
                {

                    var result = await _userService.CheckPasswordSignInAsync(user, request.Password);

                    if (!result)
                    {
                        return Unauthorized();
                    }

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Token:Issuer"],
                        audience: _configuration["Token:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                                    (Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                                SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }

            return BadRequest();
        }
    }
}
