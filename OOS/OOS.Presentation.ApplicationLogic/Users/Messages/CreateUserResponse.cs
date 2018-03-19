using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class CreateUserResponse : ResponseBase
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Boolean Gender { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }
    }
}
