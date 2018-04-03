using OOS.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class GetProductsRequest : ListQueryBase
    {
        public string Name { get; set; }
    }
}
