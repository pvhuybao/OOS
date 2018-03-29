using MongoDB.Bson.Serialization.Attributes;
using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Contacts.Models
{
    public class Email
    {
        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }
    }
}
