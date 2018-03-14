using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OOS.Domain.Categories.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Categories.Messages;

namespace OOS.Presentation.ApplicationLogic.Categories
{
    public class CategoriesBusinessLogic : ICategoriesBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public CategoriesBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public CreateCategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            var result = new CreateCategoryResponse();

            var cate = _mapper.Map<CreateCategoryRequest, Category>(request);
            cate.Id = Guid.NewGuid().ToString();

            _mongoDbRepository.Create<Category>(cate);
            return result;
        }
    }
}
