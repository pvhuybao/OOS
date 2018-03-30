using OOS.Shared.Enums;
using System.Collections.Generic;

namespace OOS.Presentation.ApplicationLogic.Configurations.Messages
{
    public class ConfigurationRequest
    {
        public List<string> Carousel { get; set; }
        public Currency Currency { get; set; }
        public string ShippingReturnHtml { get; set; }
        public string ShippingGuideHtml { get; set; }
        public string FaqHtml { get; set; }
    }
}
