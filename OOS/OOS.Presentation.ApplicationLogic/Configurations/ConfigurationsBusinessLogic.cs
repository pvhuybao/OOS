using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Configuration.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Configurations.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Configurations
{
    public class ConfigurationsBusinessLogic:IConfigurationsBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public ConfigurationsBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }
        public Configuration GetConfiguration()
        {
            var config = _mongoDbRepository.Find<Configuration>().FirstOrDefault();
            return config==null?new Configuration():config;
        }
        public ConfigurationResponse CreateConfiguration(ConfigurationRequest request)
        {
            var config = _mapper.Map<ConfigurationRequest, Configuration>(request);
            config.Id = "1";

            _mongoDbRepository.Create(config);

            var result = _mapper.Map<Configuration, ConfigurationResponse>(config);

            return result;
        }
        public ConfigurationResponse EditConfiguration(ConfigurationRequest request, string id)
        {
            var Configuration = _mapper.Map<ConfigurationRequest, Configuration>(request);
            Configuration.Id = id;
            _mongoDbRepository.Replace<Configuration>(Configuration);
            var result = _mapper.Map<Configuration, ConfigurationResponse>(Configuration);
            return result;
        }
    }
}
