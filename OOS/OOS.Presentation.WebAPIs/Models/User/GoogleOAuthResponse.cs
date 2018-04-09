using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Models.User
{
    public class GoogleOAuthResponse
    {
        public string email { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }

    }
}
