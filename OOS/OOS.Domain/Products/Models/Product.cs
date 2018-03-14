using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Domain.Products.Models
{
    [BsonIgnoreExtraElements]
    public class Product : IAggregateRoot
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string IdCategory { get; set; }

        public DateTime Createdate { get; set; }

        public string Createby { get; set; }

        public DateTime Updatedate { get; set; }

        public string Updateby { get; set; }

        public string Status { get; set; }
    }
}