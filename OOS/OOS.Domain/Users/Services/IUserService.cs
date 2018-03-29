using Microsoft.AspNetCore.Identity;
using OOS.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOS.Domain.Users.Services
{
    public interface IUserService
    {
        Task<IdentityResult> SignUpAsync(User user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure = false);

        Task SignOutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);

        Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByIdAsync(string id);

        string GenerateAppToken(User user);
    }
}
