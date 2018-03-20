using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using OOS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace OOS.Domain.Categories.Models
{
    [BsonIgnoreExtraElements]
    public class Category : IAggregateRoot
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
 
        public string Description { get; set; }

        public CategoryStatus Status { get; set; }    
    }
   
}