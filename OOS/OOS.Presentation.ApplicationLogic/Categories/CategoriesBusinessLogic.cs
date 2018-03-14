using AutoMapper;
using OOS.Domain.Categories.Models;
using OOS.Infrastructure.Mongodb;

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

        public void DeleteCategory(string id)
        {
            var category = _mongoDbRepository.Get<Category>(id);
            _mongoDbRepository.Delete(category);
        }
    }
}
