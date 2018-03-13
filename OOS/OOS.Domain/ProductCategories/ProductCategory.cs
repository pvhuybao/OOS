using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.ProductCategories
{
    [BsonIgnoreExtraElements]
    public class ProductCategory : IAggregateRoot
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}