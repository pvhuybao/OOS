using OOS.Domain.Configuration.Models;
using OOS.Presentation.ApplicationLogic.Configurations.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Configurations
{
    public interface IConfigurationsBusinessLogic
    {
        Configuration GetConfiguration();
        ConfigurationResponse EditConfiguration(ConfigurationRequest request, string id);
        ConfigurationResponse CreateConfiguration(ConfigurationRequest request);
    }
}
