using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using OOS.Shared.Enums;
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

        public string UserId { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public List<Address> Address { get; set; }

        public double Total { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public OrderStatus Status { get; set; }
    }
}
