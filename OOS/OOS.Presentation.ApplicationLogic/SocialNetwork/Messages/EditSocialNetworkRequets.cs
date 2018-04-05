using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.SocialNetwork.Messages
{
    public class EditSocialNetworkRequets
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string Hostline { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
