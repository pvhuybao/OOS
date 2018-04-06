using AutoMapper;
using OOS.Domain.Contacts.Models;
using OOS.Domain.CustomerFeedback.Models;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using OOS.Presentation.ApplicationLogic.CustomerFeedback;
using OOS.Presentation.ApplicationLogic.CustomerFeedback.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public class FeedbackBusinessLogicAutoMapper : Profile
    {
        public FeedbackBusinessLogicAutoMapper()
        {
            CreateMap<CreateFeedbackRequest, Feedback>();
            CreateMap<EditFeedBackRequest, Feedback>();
            CreateMap<Feedback, EditFeedBackResponse>();
        }
    }
}