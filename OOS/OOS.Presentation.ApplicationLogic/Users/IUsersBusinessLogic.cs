using OOS.Domain.Users.Models;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users
{
    public interface IUsersBusinessLogic
    {
        List<User> GetUser();
        User GetUser(string id);
        CreateUserResponse CreateUser(CreateUserRequest request);
        void DeleteUser(string id);
    }
}
