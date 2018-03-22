using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts.Messages
{
    public class SentEmailRequest: RequestBase
    {
        public string toEmail { set; get; }

        public string subject { set; get; }

        public string content { set; get; }
    }
}
