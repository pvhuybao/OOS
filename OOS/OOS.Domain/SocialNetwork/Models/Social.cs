using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.SocialNetwork.Models
{
    [BsonIgnoreExtraElements]
    public class Social : IAggregateRoot
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string Hostline { get; set; }

        public string Email { get; set; }

        public string linkFB { get; set; }

        public string linkTwitter { get; set; }

        public string linkInstagram { get; set; }

        public string linkGooglePlus {get;set;}

        public string linkPinterest { get; set; }

}
}
