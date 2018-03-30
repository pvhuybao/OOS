using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Contacts.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;


namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public class EmailBusinessLogic : IEmailBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public EmailBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public CreateEmailSubscribeResponse CreateEmailSubscribe(CreateEmailSubscribeRequest request)
        {
            var result = new CreateEmailSubscribeResponse();
            var email = _mapper.Map<CreateEmailSubscribeRequest, EmailSubsribe>(request);
            email.Id = Guid.NewGuid().ToString();
            _mongoDbRepository.Create(email);
            return result;
        }

        public void DeleteEmailSubsribe(string id)
        {
            var deleteEmailSubsribe = _mongoDbRepository.Get<EmailSubsribe>(id);
            _mongoDbRepository.Delete(deleteEmailSubsribe);
        }

        public List<EmailSubsribe> GetEmailSubsribe()
        {
            List<EmailSubsribe> listEmailSubsribe = new List<EmailSubsribe>();
            var filter = Builders<EmailSubsribe>.Filter.Empty;
            listEmailSubsribe.AddRange(_mongoDbRepository.Find(filter).ToList().OrderByDescending(t => t.CreatedDate));
            return listEmailSubsribe;
        }

        public SentEmailResponse SentEmail(SentEmailRequest request)
        {
            var result = new SentEmailResponse();
            string senderID = "dmthanh572@gmail.com";
            string senderPassword = "tau0bieT";
            string emailAdmin = "nguyenhuuloc304@gmail.com";

            string body = "Feedback from " + request.ToEmail +"<br>";
            body += request.Content;
            MailMessage mail = new MailMessage();
            mail.To.Add(emailAdmin);
            mail.From = new MailAddress(senderID);
            mail.Subject = "Feedback: "+request.Subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";//"smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(mail);


            string body1 = "We received a feedback from you";
            MailMessage mailFeedback = new MailMessage();
            mailFeedback.To.Add(request.ToEmail);
            mailFeedback.From = new MailAddress(senderID);
            mailFeedback.Subject = "Email feedback";
            mailFeedback.Body = body1;
            mailFeedback.IsBodyHtml = true;
            smtp.Send(mailFeedback);
            return result;
        }
    }
}
