using OOS.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class GetUserRequest : ListQueryBase
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
