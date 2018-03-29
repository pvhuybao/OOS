using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Identity.MongoDB;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Users.Models
{
    [BsonIgnoreExtraElements]
    public class Role : IdentityRole<string>, IAggregateRoot
    {
        /// <summary>
        /// Permissions
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }
    }
}
