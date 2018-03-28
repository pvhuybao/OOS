using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Contacts.Models
{
    [BsonIgnoreExtraElements]
    public class EmailSubsribe: AuditableEntityBase, IAggregateRoot
    {
        public string Id { set; get; }

        public string email { set; get; }

    }
}
