using OOS.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class GetProductsRequest : ListQueryBase
    {
        public string IdCategory { get; set; }
        public string Keyword { get; set; }
        public string Sort { get; set; }
        public int MinInPrice { get; set; }
        public int MaxInPrice { get; set; }
    }
}
