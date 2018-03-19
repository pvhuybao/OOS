using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class CreateUserRequest : RequestBase
    {

        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Boolean Gender { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }
    }
}
