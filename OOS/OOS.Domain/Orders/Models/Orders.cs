using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Orders.Models
{
    [BsonIgnoreExtraElements]
    public class Orders : IAggregateRoot
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
