using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Domain.Products.Models
{
    [BsonIgnoreExtraElements]
    public class Product : AuditableEntityBase, IAggregateRoot
    {
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string IdCategory { get; set; }

        public ProductStatus? Status { get; set; }
    }
}