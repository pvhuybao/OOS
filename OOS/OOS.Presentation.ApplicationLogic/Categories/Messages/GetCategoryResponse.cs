using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;
using OOS.Domain.Categories.Models;
using OOS.Shared.Enums;

namespace OOS.Presentation.ApplicationLogic.Categories.Messages
{
    public class GetCategoryResponse : ResponseBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryStatus Status { get; set; }
    }
}
