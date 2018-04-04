using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Users.Models;
using OOS.Domain.Users.Services;
using OOS.Infrastructure.Helpers;
using OOS.Infrastructure.Mongodb;
using OOS.Infrastructure.Queries;
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

        public PagedQueryResult<GetUserResponse> GetUser(GetUserRequest request)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Empty;

            if (!String.IsNullOrEmpty(request.Email))
            {
                filter = filter & builder.Where(it => it.Email.Equals(request.Email));
            }

            if (!String.IsNullOrEmpty(request.Phone))
            {
                filter = filter & builder.Where(it => it.PhoneNumber.Equals(request.Phone));
            }

            if (!String.IsNullOrEmpty(request.Username))
            {
                filter = filter & builder.Where(it => it.UserName.Equals(request.Username));
            }

            var listUsers = _mongoDbRepository.Find(filter);

            var totalItemCount = listUsers.Count();

            var usersOverview = _mapper.Map<IEnumerable<GetUserResponse>>(
                listUsers
                .SortByDescending(it => it.CreatedDate)
                .Skip((request.Page - 1) * request.PageSize)
                .Limit(request.PageSize)
                .ToList());

            var pagedResult = new PagedQueryResult<GetUserResponse>(usersOverview, totalItemCount, request.Page, request.PageSize);
            return pagedResult;
        }

        public User GetUser(string id)
        {
            var user = _mongoDbRepository.Get<User>(id);
            return user;
        }

        public User GetUserByName(string name)
        {
            var filter = Builders<User>.Filter.Where(u => u.UserName == name);
            var user = _mongoDbRepository.Find<User>(filter).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Where(u => u.Email == email);
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
