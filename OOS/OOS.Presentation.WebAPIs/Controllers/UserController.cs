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
using OOS.Presentation.WebAPIs.Models.User;
using AutoMapper;
using OOS.Domain.Users.Models;
using OOS.Infrastructure.Identity.MongoDB;
using OOS.Presentation.WebAPIs.Models.Manager;
namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUsersBusinessLogic _usersBusinessLogic;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


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
            var user = _userService.FindByIdAsync(id).Result;
            if (user != null)
            {

                return Ok(user);
            }

            return BadRequest();
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]CreateUserRequest request)
        {
            var rs = _usersBusinessLogic.CreateUser(request);
            return Ok(rs);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel value)
        {
            var result = await _userService.SignInAsync(value.Email, value.Password, value.RememberMe, lockoutOnFailure: false);
            var userRespone = new UserResponse();
            if (result.Succeeded)
            {
                var user = _userService.FindByEmailAsync(value.Email);
                userRespone.UserName = user.Result.UserName;
                userRespone.Email = value.Email;
                userRespone.Token = null;
            }
            return Ok(userRespone);
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
        
        [HttpGet("CheckUser/{username}")]
        public IActionResult CheckUserByUserName(string username)
        {
            var user = _usersBusinessLogic.GetUserByName(username);
            return Ok(user);
        }

        [HttpGet("CheckUserEmail/{email}")]
        public IActionResult CheckUserByEmail(string email)
        {
            var user = _usersBusinessLogic.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            //var user = _mapper.Map<RegisterViewModel, User>(model);
            var user = new User { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Gender = model.Gender };
            var result = _userService.SignUpAsync(user, model.Password);
            //var response = _mapper.Map<User, CreateUserResponse>(user);
            return Ok(result);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody]EditViewModel model)
        {

            var user = _userService.FindByEmailAsync(model.Email).Result;
            
            if (user != null)
            {
                user.UserName = model.Username;
                user.Gender = model.Gender;
                user.UserType = model.UserType;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.Country = model.Country;
                user.PreferredLanguage = model.PreferredLanguage;
                user.DateOfBirth = model.DateOfBirth;
                user.Photo = model.Photo;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userService.UpdateUserAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred updating profile for user ID '{user.Id}'.");
                }

                return Ok(new EditViewModelResponse() { });
            }

            return BadRequest();
        }
    }
}
