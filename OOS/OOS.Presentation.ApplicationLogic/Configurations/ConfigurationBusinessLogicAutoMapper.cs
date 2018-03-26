using AutoMapper;
using OOS.Domain.Configuration.Models;
using OOS.Presentation.ApplicationLogic.Configurations.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Configurations
{
    public class ConfigurationBusinessLogicAutoMapper : Profile
    {
        public ConfigurationBusinessLogicAutoMapper()
        {
            CreateMap<ConfigurationRequest, Configuration>();
            CreateMap<Configuration, ConfigurationResponse>();
        }
    }
}
