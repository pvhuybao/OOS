using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public interface IEmailBusinessLogic
    {
        SentEmailResponse SentEmail (SentEmailRequest request);
    }
}
