using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OOS.Domain.Categories.Models;
using OOS.Shared.Enums;

namespace OOS.Presentation.ApplicationLogic.Categories.Messages
{
    public class CreateCategoryRequest : RequestBase
    {
        [Required(ErrorMessage = "Please enter {0}!")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} contains {2}-{1} characters!")]
        public string Name { get; set; }
        
        [StringLength(250, ErrorMessage = "{0} can't over {1} characters!")]
        public string Description { get; set; }

        public CategoryStatus Status { get; set; }
    }
}
