using AutoMapper;
using OOS.Domain.SocialNetwork.Models;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.SocialNetwork.Messages;
using OOS.Presentation.ApplicationLogic.SocialNetworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.SocialNetwork
{
    public class SocialNetworkBusinessLogic : ISocialNetworkBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;
        private object request;

        public SocialNetworkBusinessLogic(IMapper mapper,IMongoDbRepository mongoDbRepository)
        {
            _mapper = mapper;
            _mongoDbRepository = mongoDbRepository;
        }

        public CreateSocialNetworkReponse CreateSocialNetwork(CreateSocialNetworkRequets requets)
        {
            var result = new CreateSocialNetworkReponse();
            var pro = _mapper.Map<CreateSocialNetworkRequets, Social>(requets);
            pro.Id = "a1";
            _mongoDbRepository.Create<Social>(pro);
            return result;
        }

        public EditSocialNetworkRepones EditSocialNetwork(string id, EditSocialNetworkRequets request)
        {
            var pro = _mapper.Map<EditSocialNetworkRequets, Social>(request);
            pro.Id = id;

            _mongoDbRepository.Replace<Social>(pro);

            var result = _mapper.Map<Social, EditSocialNetworkRepones>(pro);
            return result;

        }

        public Social GetSocial(string id)
        {
            return _mongoDbRepository.Get<Social>(id);
        }
    }
}
