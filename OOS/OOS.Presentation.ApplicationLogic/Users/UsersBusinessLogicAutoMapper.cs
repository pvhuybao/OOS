using AutoMapper;
using OOS.Domain.Users.Models;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users
{
    public class UsersBusinessLogicAutoMapper : Profile
    {
        public UsersBusinessLogicAutoMapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<EditUserRequest, User>();
            CreateMap<User, EditUserRequest>();
            CreateMap<User, CreateUserRequest>();
        }

    }
}
