using OOS.Domain.Contacts.Models;
using OOS.Domain.CustomerFeedback.Models;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using OOS.Presentation.ApplicationLogic.CustomerFeedback.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.CustomerFeedback
{
    public interface IFeedbackBusinessLogic
    {
        Feedback GetFeedBack(string id);

        List<Feedback> GetEmailFeedBack();

        void DeleteFeedback(string id);

        CreateFeedbackResponse CreateFeedBack(CreateFeedbackRequest request);

        EditFeedBackResponse EditFeedBack(string id, EditFeedBackRequest request);

    }
}
