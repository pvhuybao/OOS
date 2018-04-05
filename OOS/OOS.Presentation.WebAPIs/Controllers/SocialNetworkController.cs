using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.SocialNetwork.Messages;
using OOS.Presentation.ApplicationLogic.SocialNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/SocialNetwork")]
    public class SocialNetworkController : Controller
    {
        private readonly ISocialNetworkBusinessLogic _socialNetworkBusinessLogic;

        public SocialNetworkController(ISocialNetworkBusinessLogic socialNetworkBusinessLogic)
        {
            _socialNetworkBusinessLogic = socialNetworkBusinessLogic;
        }


        // GET: api/Configuration
        [HttpGet]
        public IActionResult GetSocial(string id)
        {
            var config = _socialNetworkBusinessLogic.GetSocial(id);
            return Ok(config);
        }

        // POST: api/SocialNetwork
        [HttpPost]
        public IActionResult Post([FromBody]CreateSocialNetworkRequets request)
        {
            var rs = _socialNetworkBusinessLogic.CreateSocialNetwork(request);
            return Ok(rs);
        }

        // PUT: api/SocialNetwork/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]EditSocialNetworkRequets request)
        {
            var rs = _socialNetworkBusinessLogic.EditSocialNetwork(id, request);
            return Ok(rs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _socialNetworkBusinessLogic.GetSocial(id);
            return Ok(result);
        }
    }
}
