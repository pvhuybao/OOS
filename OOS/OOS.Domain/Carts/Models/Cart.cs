﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using OOS.Infrastructure.Mongodb;
using OOS.Infrastructure.Domain;

namespace OOS.Domain.Carts.Models
{
    [BsonIgnoreExtraElements]
    public class Cart : IAggregateRoot
    {
        public string Id { get; set; }
     
        public string Email { get; set; }

        public string UserId { get; set; }

        public List<CartDetails> CartDetails { get; set; }

    }
}