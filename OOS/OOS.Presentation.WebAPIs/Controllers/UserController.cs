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
        public IActionResult Get(GetUserRequest request)
        {
            var listUser = _usersBusinessLogic.GetUser(request);
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
        public async Task<IActionResult> Post([FromBody] RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Photo = model.Image,
                WishList = new List<string>()
            };
            var result = await _userService.SignUpAsync(user, model.Password);
            return Ok(result);
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
                userRespone.Id = user.Result.Id;
                userRespone.Username = user.Result.UserName;
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
        public async Task<IActionResult> Put(string id, [FromBody]EditViewModel request)
        {
            var user = _userService.FindByIdAsync(id).Result;
            if (user != null)
            {
                user.UserName = request.Username;
                user.Gender = request.Gender;
                user.UserType = request.UserType;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.MiddleName = request.MiddleName;
                user.Country = request.Country;
                user.PreferredLanguage = request.PreferredLanguage;
                user.DateOfBirth = request.DateOfBirth;
                user.Photo = request.Photo;
                user.PhoneNumber = request.PhoneNumber;

                var result = await _userService.UpdateUserAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred updating profile for user ID '{user.Id}'.");
                }

                return Ok(new EditViewModelResponse() { });
            }

            return BadRequest();
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

        [HttpGet("CheckUserExist/{terms}")]
        public IActionResult CheckUserExist(string terms)
        {
            var user = _usersBusinessLogic.CheckUser(terms);
            return Ok(user);
        }

        [HttpPost("{id}/product/{idProduct}/addWishProduct")]
        public async Task<IActionResult> AddWishProduct(string id, string idProduct)
        {
            var user = _userService.FindByIdAsync(id).Result;
            if (user != null)
            {
                user.WishList.Add(idProduct);
                var result = await _userService.UpdateUserAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred Adding wish product for user ID '{user.Id}'.");
                }
                return Ok();
            }
            return BadRequest();
        }

    }
}
