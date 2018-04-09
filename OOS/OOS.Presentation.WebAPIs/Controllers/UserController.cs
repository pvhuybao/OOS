using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Users;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using OOS.Presentation.ApplicationLogic.Products;
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
using OOS.Domain.Products.Models;
using OOS.Infrastructure.Identity.MongoDB;
using OOS.Presentation.WebAPIs.Models.Manager;
using OOS.Presentation.ApplicationLogic.Products.Messages;
using System.Net.Http;
using Newtonsoft.Json;
using OOS.Shared.Enums;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUsersBusinessLogic _usersBusinessLogic;
        private readonly IProductsBusinessLogic _productsBusinessLogic;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private static readonly HttpClient Client = new HttpClient();

        public UserController(IUsersBusinessLogic UsersBusinessLogic, IUserService userService, IConfiguration configuration, IProductsBusinessLogic productsBusinessLogic)
        {
            _usersBusinessLogic = UsersBusinessLogic;
            _userService = userService;
            _configuration = configuration;
            _productsBusinessLogic = productsBusinessLogic;
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
            var user = _userService.FindByEmailAsync(terms).Result;
            return Ok(user);
        }

        [HttpGet("GetWishList/{userId}")]
        public IActionResult GetWishList(string userId)
        {
            var user = _userService.FindByIdAsync(userId).Result;
            List<GetProductExtraCategoryNameResponse> productWishList = new List<GetProductExtraCategoryNameResponse>();
            foreach (var item in user.WishList)
            {
                productWishList.Add(_productsBusinessLogic.GetProduct(item));
            }
            return Ok(productWishList);
        }

        [HttpDelete("{id}/product/{idProduct}/removeWishProduct")]
        public async Task<IActionResult> RemoveWishProduct(string id, string idProduct)
        {
            var user = _userService.FindByIdAsync(id).Result;
            if (user != null)
            {
                user.WishList.Remove(idProduct);
                var result = await _userService.UpdateUserAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred Adding wish product for user ID '{user.Id}'.");
                }
                return Ok();
            }
            return BadRequest();
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

        [HttpPost("{id}/product/{idProduct}/checkWishProduct")]
        public async Task<IActionResult> CheckWishProduct(string id, string idProduct)
        {
            var user = _userService.FindByIdAsync(id).Result;
            if (user != null)
            {
                var product = user.WishList.Find(p => p == idProduct);
                var response = new CheckWishProductResponse();
                if (product != null)
                {
                    response.IsWishProduct = true;
                }
                else
                {
                    response.IsWishProduct = false;
                }
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("LoginFacebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userService.FindByEmailAsync(userInfo.Email);
            User userFacebook = new User();
            if (user == null)
            {  
                userFacebook.FirstName = userInfo.FirstName;
                userFacebook.LastName = userInfo.LastName;
                userFacebook.Email = userInfo.Email;
                userFacebook.Photo = userInfo.Picture.Data.Url;
                userFacebook.UserName = userInfo.Email;
                userFacebook.Gender =(GenderType)Enum.Parse(typeof(GenderType), userInfo.Gender, ignoreCase: true) ;
                userFacebook.Country = userInfo.Locale;
                string password = "Eshop123!";
                var result = await _userService.SignUpAsync(userFacebook, password);
                
                if (result.Succeeded == false) return BadRequest();
                return Ok(userFacebook);

            }
            return Ok(user);

        }
        [HttpPost]
        [Route("LoginGoogle")]
        public async Task<IActionResult> Google([FromBody]string token)
        {
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={token}");
            var userInfo = JsonConvert.DeserializeObject<GoogleOAuthResponse>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userService.FindByEmailAsync(userInfo.email);
            User userGoogle = new User();
            if (user == null)
            {
                userGoogle.FirstName = userInfo.given_name;
                userGoogle.LastName = userInfo.family_name;
                userGoogle.Email = userInfo.email;
                userGoogle.Photo = userInfo.picture;
                userGoogle.UserName = userInfo.email;
                userGoogle.Gender = 0;
                userGoogle.Country = userInfo.locale;
                string password = "Eshop123!";
                var result = await _userService.SignUpAsync(userGoogle, password);

                if (result.Succeeded == false) return BadRequest();
                return Ok(userGoogle);

            }
            return Ok(user);
        }
    }
}
