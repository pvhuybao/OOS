using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts.Messages
{
    public class SentEmailRequest: RequestBase
    {
        public string Id { set; get; }

        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }

        public Boolean Status = (false);
    }
}
