using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.CustomerFeedback.Models
{
    public class Feedback : AuditableEntityBase, IAggregateRoot
    {
        public string Id { set; get; }

        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }

        public Boolean Status { set; get; }
    }
}
