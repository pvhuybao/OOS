using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products
{
    public interface IUsersBusinessLogic
    {
        CreateUserResponse CreateUser(CreateUserRequest request);
        EditUserResponse EditUser(EditUserRequest request, string id);
    }
}
