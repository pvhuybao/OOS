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
        public string toEmail { set; get; }

        public string subject { set; get; }

        public string content { set; get; }
    }
}
