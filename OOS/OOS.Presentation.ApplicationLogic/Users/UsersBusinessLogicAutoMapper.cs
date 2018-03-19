using AutoMapper;
using OOS.Domain.Users.Models;
using OOS.Presentation.ApplicationLogic.Users.Messages;

namespace OOS.Presentation.ApplicationLogic.Users
{
    public class UsersBusinessLogicAutoMapper : Profile
    {
        public UsersBusinessLogicAutoMapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<EditUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
            CreateMap<User, EditUserResponse>();
        }
    }
}