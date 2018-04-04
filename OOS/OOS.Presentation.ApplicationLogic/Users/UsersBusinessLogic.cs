using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Users.Models;
using OOS.Domain.Users.Services;
using OOS.Infrastructure.Helpers;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users
{
    public class UsersBusinessLogic : IUsersBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;
        private readonly IUserService _userService;

        public UsersBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository, IUserService userService)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
            _userService = userService;
        }

        public List<User> GetUser()
        {
            var filter = Builders<User>.Filter.Empty;
            var listUser = _mongoDbRepository.Find(filter).ToList();
            return listUser;
        }

        public User GetUser(string id)
        {
            var user = _mongoDbRepository.Get<User>(id);
            return user;
        }

        public User CheckUser(string term)
        {
            var filter = Builders<User>.Filter.Where(u => u.UserName == term || u.Email == term);
            var user = _mongoDbRepository.Find<User>(filter).FirstOrDefault();
            return user;
        }
        
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var user = _mapper.Map<CreateUserRequest, User>(request);
            user.Id = Guid.NewGuid().ToString();
            _mongoDbRepository.Create(user);
            var result = _mapper.Map<User, CreateUserResponse>(user);
            return result;
        }

        public EditUserResponse EditUser(EditUserRequest request, string id)
        {
            var user = _mapper.Map<EditUserRequest, User>(request);
            user.Id = id;
            _mongoDbRepository.Replace<User>(user);
            var result = _mapper.Map<User, EditUserResponse>(user);
            return result;
        }

        public void DeleteUser(string id)
        {
            var User = _mongoDbRepository.Get<User>(id);
            _mongoDbRepository.Delete(User);
        }

    }
}
