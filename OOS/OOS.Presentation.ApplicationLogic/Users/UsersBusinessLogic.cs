using OOS.Domain.Users.Models;
using OOS.Presentation.ApplicationLogic.Products;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Users.Messages;

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

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var result = new CreateUserResponse();
            var user = _mapper.Map<CreateUserRequest, User>(request);
            user.Id = Guid.NewGuid().ToString();

            _mongoDbRepository.Create(user);
            return result;
        }
    }
}
