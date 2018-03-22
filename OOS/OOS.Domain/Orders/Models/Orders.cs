using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Domain.Orders.Models
{
    [BsonIgnoreExtraElements]
    public class Orders : AuditableEntityBase, IAggregateRoot
    {
        public string Id { get; set; }

        public string IdBill { get; set; } 

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public List<Address> Address { get; set; }

        public double Total { get; set; }

        public OrderStatus Status { get; set; }
    }
}
