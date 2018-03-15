using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Categories.Messages
{
    public class GetCategoryResponse : ResponseBase
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
