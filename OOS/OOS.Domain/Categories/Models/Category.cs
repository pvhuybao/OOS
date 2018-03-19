using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
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

        public Status Status { get; set; }



    }

    public enum Status
    {
        [Display(Name = "Còn hàng")]
        Status1,
        [Display(Name = "Hết hàng")]
        Status2,
        [Display(Name = "Mới")]
        Status3,
        [Display(Name = "sadasd")]
        Status4
    }
}