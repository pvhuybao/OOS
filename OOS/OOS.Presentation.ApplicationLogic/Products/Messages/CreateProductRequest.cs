using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class CreateProductRequest : RequestBase
    {
        public string Name { get; set; }
    }
}
