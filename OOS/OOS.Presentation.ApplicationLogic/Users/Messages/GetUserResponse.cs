﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Users.Messages
{
    public class GetUserResponse
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public UserType UserType { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public GenderType Gender { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(MiddleName))
                {
                    return String.Format("{0} {1}", FirstName, LastName);
                }

                return String.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
            }
        }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Preferred language
        /// </summary>
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public UserStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
