using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.CustomerFeedback.Messages
{
   public class CreateFeedbackRequest
    {

        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }

    }
}
