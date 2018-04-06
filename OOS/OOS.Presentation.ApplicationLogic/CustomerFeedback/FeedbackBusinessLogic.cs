using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AutoMapper;
using MongoDB.Driver;
using OOS.Domain.Contacts.Models;
using OOS.Domain.CustomerFeedback.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using OOS.Presentation.ApplicationLogic.CustomerFeedback;
using OOS.Presentation.ApplicationLogic.CustomerFeedback.Messages;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public class FeedbackBusinessLogic : IFeedbackBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public FeedbackBusinessLogic(IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public Feedback GetFeedBack(string id)
        {
            return _mongoDbRepository.Get<Feedback>(id);
        }

        public List<Feedback> GetEmailFeedBack()
        {
            var data = _mongoDbRepository.Find<Feedback>().ToList();
            return data;
        }

        public void DeleteFeedback(string id)
        {
            var feed = _mongoDbRepository.Get<Feedback>(id);
            _mongoDbRepository.Delete(feed);
        }

        public CreateFeedbackResponse CreateFeedBack(CreateFeedbackRequest request)
        {
            var result = new CreateFeedbackResponse();
            var Feed = _mapper.Map<CreateFeedbackRequest, Feedback>(request);
            Feed.Id = Guid.NewGuid().ToString();
            _mongoDbRepository.Create<Feedback>(Feed);
            return result;
        }

        public EditFeedBackResponse EditFeedBack(string id, EditFeedBackRequest request)
        {
            var feed = _mapper.Map<EditFeedBackRequest, Feedback>(request);
            feed.Id = id;
            _mongoDbRepository.Replace<Feedback>(feed);
            var result = _mapper.Map<Feedback, EditFeedBackResponse>(feed);
            return result;
        }      
    }
}
