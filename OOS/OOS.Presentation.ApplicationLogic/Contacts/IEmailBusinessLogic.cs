using OOS.Domain.Contacts.Models;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using OOS.Presentation.ApplicationLogic.CustomerFeedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public interface IEmailBusinessLogic
    {
       
        SentEmailResponse SentEmail (SentEmailRequest request);

        List<EmailSubsribe> GetEmailSubsribe();
 
        CreateEmailSubscribeResponse CreateEmailSubscribe(CreateEmailSubscribeRequest request);

        void DeleteEmailSubsribe(string id);

        Feedback GetFeedBack(string id);

        List<Feedback> GetEmailFeedBack();

        void DeleteFeedback(string id);

        SentEmailResponse CreateFeedBack(SentEmailRequest request);

        EditFeedBackResponse EditFeedBack(string id, EditFeedBackRequest request);

    }
}
