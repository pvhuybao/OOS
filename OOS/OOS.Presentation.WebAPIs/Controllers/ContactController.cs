using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Contacts;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IEmailBusinessLogic _emailsBusinessLogic;

        public ContactController(IEmailBusinessLogic emailsBusinessLogic)
        {
            _emailsBusinessLogic = emailsBusinessLogic;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var listEmailSubsribe = _emailsBusinessLogic.GetEmailSubsribe();
            return Ok(listEmailSubsribe);
        }

        [Route("getFeedBack")]
        [HttpGet]
        public IActionResult GetFeedBack()
        {
            var listEmailFeedBack = _emailsBusinessLogic.GetEmailFeedBack();
            return Ok(listEmailFeedBack);
        }

        // GET api/<controller>/5
        [Route("getFeedBack/{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            var result = _emailsBusinessLogic.GetFeedBack(id);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] SentEmailRequest value)
        {
            _emailsBusinessLogic.SentEmail(value);
            _emailsBusinessLogic.CreateFeedBack(value);
            return Ok();
        }

        [Route("emailSubsribe")]
        [HttpPost]
        public IActionResult EmailSubsribe([FromBody] CreateEmailSubscribeRequest value)
        {
            _emailsBusinessLogic.CreateEmailSubscribe(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _emailsBusinessLogic.DeleteEmailSubsribe(id);
            return Ok();
        }

    }
}
