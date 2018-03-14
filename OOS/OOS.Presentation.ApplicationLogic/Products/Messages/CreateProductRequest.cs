using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class CreateProductRequest : RequestBase
    {
        [Required]
        public string Name { get; set; }
    }
}
