using OOS.Presentation.ApplicationLogic.Common;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class CreateUserRequest : RequestBase
    {
        [Required(ErrorMessage = "Please enter {0}!")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "{0} contains {2}-{1} characters!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter {0}!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "{0} contains {2}-{1} characters!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter {0}!")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Boolean Gender { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Please enter {0}!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }
    }
}
