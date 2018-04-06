using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Contacts;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using OOS.Presentation.ApplicationLogic.CustomerFeedback;
using OOS.Presentation.ApplicationLogic.CustomerFeedback.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackBusinessLogic _feeedbackBusinessLogic;

        public FeedbackController(IFeedbackBusinessLogic feeedbackBusinessLogic)
        {
            _feeedbackBusinessLogic = feeedbackBusinessLogic;
        }
        [HttpGet]
        public IActionResult GetFeedBack()
        {
            var listEmailFeedBack = _feeedbackBusinessLogic.GetEmailFeedBack();
            return Ok(listEmailFeedBack);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _feeedbackBusinessLogic.GetFeedBack(id);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] CreateFeedbackRequest value)
        {
            _feeedbackBusinessLogic.CreateFeedBack(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(string id)
        {
            _feeedbackBusinessLogic.DeleteFeedback(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutFeedBack(string id, [FromBody] EditFeedBackRequest request)
        {
            var rs = _feeedbackBusinessLogic.EditFeedBack(id, request);
            return Ok(rs);
        }


    }
}
    
 
