using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.SocialNetwork.Models
{
    [BsonIgnoreExtraElements]
    public class Social :IAggregateRoot
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string Hostline { get; set; }

        public string Email { get; set; }
    }
}
