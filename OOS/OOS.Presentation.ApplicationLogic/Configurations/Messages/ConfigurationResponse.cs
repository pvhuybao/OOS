using OOS.Shared.Enums;
using System.Collections.Generic;

namespace OOS.Presentation.ApplicationLogic.Configurations.Messages
{
    public class ConfigurationResponse
    {
        public string Id { get; set; }
        public List<string> Carousel { get; set; }
        public Currency Currency { get; set; }
    }
}
