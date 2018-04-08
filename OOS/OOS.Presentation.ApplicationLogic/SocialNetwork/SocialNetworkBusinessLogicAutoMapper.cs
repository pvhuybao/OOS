using AutoMapper;
using OOS.Domain.SocialNetwork.Models;
using OOS.Presentation.ApplicationLogic.SocialNetwork.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.SocialNetwork
{
    public class SocialNetworkBusinessLogicAutoMapper : Profile
    {
        public SocialNetworkBusinessLogicAutoMapper()
        {
            CreateMap<CreateSocialNetworkRequets, Social>();
            CreateMap<EditSocialNetworkRequets, Social>();
            CreateMap<Social, CreateSocialNetworkReponse>();
            CreateMap<Social, EditSocialNetworkRepones>();
           
        }
    }
}
