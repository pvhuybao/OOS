using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class CreateUserRequest : RequestBase
    {
        [Required]
        public string Name { get; set; }
    }
}
