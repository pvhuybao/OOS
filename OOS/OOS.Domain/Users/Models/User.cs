using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Domain.Users.Models
{
    [BsonIgnoreExtraElements]
    public class User : IAggregateRoot
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