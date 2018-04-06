using OOS.Domain.SocialNetwork.Models;
using OOS.Presentation.ApplicationLogic.SocialNetwork.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.SocialNetworks
{   public interface ISocialNetworkBusinessLogic
    {
        CreateSocialNetworkReponse CreateSocialNetwork(CreateSocialNetworkRequets request);

        EditSocialNetworkRepones EditSocialNetwork(string id, EditSocialNetworkRequets Request);

        Social GetSocial(string id);
    }
}
