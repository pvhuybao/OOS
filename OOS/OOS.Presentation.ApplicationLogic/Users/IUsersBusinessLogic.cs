using OOS.Domain.Users.Models;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users
{
    public interface IUsersBusinessLogic
    {
        CreateUserResponse CreateUser(CreateUserRequest request);
        EditUserResponse EditUser(EditUserRequest request, string id);
        List<User> GetUser();
        User GetUser(string id);
        User GetUserByName(string name);
        User GetUserByEmail(string email);
        void DeleteUser(string id);
    }
}
