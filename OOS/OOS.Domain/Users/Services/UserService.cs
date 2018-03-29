using Microsoft.AspNetCore.Identity;
using OOS.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOS.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<IdentityResult> SignUpAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }

        public Task<SignInResult> SignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure = false)
        {
            return _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
        {
            return _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            return _userManager.ChangeEmailAsync(user, newEmail, token);
        }

        public Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<User> FindByIdAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public string GenerateAppToken(User user)
        {
            throw new NotImplementedException();
        }

        //public string GenerateAppToken(User user)
        //{
        //    if (user.AppTokens == null)
        //    {
        //        user.AppTokens = new List<UserAppToken>();
        //    }

        //    var validAppToken = user.AppTokens.FirstOrDefault(it => it.ExpiryDate >= DateTime.UtcNow);
        //    if (validAppToken == null)
        //    {
        //        validAppToken = new UserAppToken()
        //        {
        //            UserId = user.Id,
        //            Token = Guid.NewGuid().ToString("N"),
        //            ExpiryDate = DateTime.UtcNow.AddDays(60)
        //        };

        //        user.AppTokens.Add(validAppToken);
        //    }
        //    else
        //    {
        //        if (String.IsNullOrWhiteSpace(validAppToken.Token))
        //        {
        //            validAppToken.Token = Guid.NewGuid().ToString("N");
        //        }

        //        validAppToken.ExpiryDate = DateTime.UtcNow.AddDays(60);
        //    }

        //    var result = _userManager.UpdateAsync(user).Result;
        //    if (!result.Succeeded)
        //    {
        //        throw new DomainException(result.Errors
        //            .Select(it => new DomainExceptionError()
        //            {
        //                Code = it.Code,
        //                Description = it.Description
        //            }));
        //    }

        //    return validAppToken.Token;
        //}
    }
}
