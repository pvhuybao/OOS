using OOS.Infrastructure.Domain;
using OOS.Infrastructure.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts.Messages
{
    public class EditFeedBackRequest : AuditableEntityBase, IAggregateRoot
    {
        public string Id { set; get; }

        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }

        public Boolean Status { set; get; }
    }
}
