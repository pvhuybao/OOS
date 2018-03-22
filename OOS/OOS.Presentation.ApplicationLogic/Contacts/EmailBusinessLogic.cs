using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public class EmailBusinessLogic : IEmailBusinessLogic
    {
        public SentEmailResponse SentEmail(SentEmailRequest request)
        {
            var result = new SentEmailResponse();
            string senderID = "dmthanh572@gmail.com";
            string senderPassword = "tau0bieT";

            string body = "Email is sent from " + request.toEmail;
            body += request.content;
            MailMessage mail = new MailMessage();
            mail.To.Add(request.toEmail);
            mail.From = new MailAddress(senderID);
            mail.Subject = request.subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";//"smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return result;
        }
    }
}
