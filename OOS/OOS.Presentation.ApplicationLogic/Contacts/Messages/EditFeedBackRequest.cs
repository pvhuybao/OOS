using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.CustomerFeedback
{
   public class EditFeedBackRequest
    {
        public string ToEmail { set; get; }

        public string Subject { set; get; }

        public string Content { set; get; }

        public Boolean Status { set; get; }
    }
}
