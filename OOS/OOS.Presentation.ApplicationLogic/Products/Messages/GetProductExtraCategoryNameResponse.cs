using OOS.Domain.Products.Models;
using OOS.Presentation.ApplicationLogic.Common;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class GetProductExtraCategoryNameResponse:ResponseBase
    {

        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IdCategory { get; set; }

        public string CategoryName { get; set; }

        public ProductStatus? Status { get; set; }

        public string Description { get; set; }


        public string Details { get; set; }

        public List<ProductTail> ProductTails { get; set; }
    }
}
