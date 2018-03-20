
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Categories.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Categories.Messages;
using OOS.Shared.Enums;

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
            var cate = _mapper.Map<CreateCategoryRequest, Category>(request);
            cate.Id = Guid.NewGuid().ToString();

            _mongoDbRepository.Create<Category>(cate);

            var result = _mapper.Map<Category, CreateCategoryResponse>(cate);
            return result;
        }

        public List<GetCategoryResponse> GetCategories()
        {
            var result = new List<GetCategoryResponse>();

            var filter = Builders<Category>.Filter.Empty;
            var data = _mongoDbRepository.Find<Category>(filter).ToList();

            result = _mapper.Map<List<Category>, List<GetCategoryResponse>>((List<Category>)data);

            return result;
        }

        public GetCategoryResponse GetCategory(GetCategoryRequest request)
        {
            var result = new GetCategoryResponse();

            result = _mapper.Map<Category, GetCategoryResponse>((Category)_mongoDbRepository.Get<Category>(request.Id));

            return result;
        }

        public List<Category> Get(CategoryStatus status)
        {
            var filter = Builders<Category>.Filter.Where(c => c.Status == status);
            var listCategory = _mongoDbRepository.Find(filter).ToList();
            return listCategory;
        }

        public void DeleteCategory(string id)
        {
            var category = _mongoDbRepository.Get<Category>(id);
            _mongoDbRepository.Delete(category);
        }

        public EditCategoryResponse EditCategory(string id, EditCategoryRequest request)
        {            
            var cate = _mapper.Map<EditCategoryRequest, Category>(request);
            cate.Id = id;

            _mongoDbRepository.Replace<Category>(cate);

            var result = _mapper.Map<Category, EditCategoryResponse>(cate);
            return result;
        }
    }
}
