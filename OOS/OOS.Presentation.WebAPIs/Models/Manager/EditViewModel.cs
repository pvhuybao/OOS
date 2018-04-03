using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Models.Manager
{
    public class EditViewModel
    {
        [Required]
        public string Username { get; set; }

        //  public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [BsonRepresentation(BsonType.String)]
        public GenderType Gender { get; set; }

        [Required]
        [BsonRepresentation(BsonType.String)]
        public UserType UserType { get; set; }

        public string Photo { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        //public string StatusMessage { get; set; }

        public string Country { get; set; }

        public string PreferredLanguage { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}
