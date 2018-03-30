using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Users.Models;
using OOS.Infrastructure.Helpers;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Users.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CreateUserResponse CreateUser(CreateUserRequest request, string password)
        {
            //validation
            var user = _mapper.Map<CreateUserRequest, User>(request);
            user.Id = Guid.NewGuid().ToString();

            var filter = Builders<User>.Filter.Where(u => u.Username == user.Username);
            if(_mongoDbRepository.Find(filter).Count() > 0)
            {
                throw new AppException("Username " + user.Username+" already exists." );
            }
            if(!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                EncodeDecodePassword.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.Password = Convert.ToBase64String(passwordSalt) + Convert.ToBase64String(passwordHash);
            }

            _mongoDbRepository.Create(user);
            
            var result = _mapper.Map<User, CreateUserResponse>(user);

            //var user = _mapper.Map<CreateUserRequest, User>(request);
            //user.Id = Guid.NewGuid().ToString();

            //_mongoDbRepository.Create(user);

            //var result = _mapper.Map<User, CreateUserResponse>(user);

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
