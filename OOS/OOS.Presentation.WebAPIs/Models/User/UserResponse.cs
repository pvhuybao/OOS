using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Models.User
{
    public class UserResponse
    {
        public string Id { set; get; }

        public string UserName { set; get; }

        public string Email { set; get; }

        public string Token { set; get; }

    }
}
