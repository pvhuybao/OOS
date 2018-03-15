using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Users.Models;
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

        public UsersBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
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

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var result = new CreateUserResponse();
            var user = _mapper.Map<CreateUserRequest, User>(request);
            user.Id = Guid.NewGuid().ToString();

            _mongoDbRepository.Create(user);
            return result;
        }
        public EditUserResponse EditUser(EditUserRequest request, string id)
        {
            var result = new EditUserResponse();
            var pro = _mapper.Map<EditUserRequest, User>(request);
            pro.Id = id;
            _mongoDbRepository.Replace<User>(pro);
            return result;
        }
        public void DeleteUser(string id)
        {
            var User = _mongoDbRepository.Get<User>(id);
            _mongoDbRepository.Delete(User);
        }
    }
}
