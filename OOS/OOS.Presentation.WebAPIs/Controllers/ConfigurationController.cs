using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOS.Presentation.ApplicationLogic.Configurations;
using OOS.Presentation.ApplicationLogic.Configurations.Messages;

namespace OOS.Presentation.WebAPIs.Controllers
{
    [Route("api/Configuration")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationsBusinessLogic _configurationsBusinessLogic;

        public ConfigurationController(IConfigurationsBusinessLogic ConfigurationsBusinessLogic)
        {
            _configurationsBusinessLogic = ConfigurationsBusinessLogic;
        }

        // GET: api/Configuration
        [HttpGet]
        public IActionResult Get()
        {
            var config = _configurationsBusinessLogic.GetConfiguration();
            return Ok(config);
        }

        // POST: api/Configuration
        [HttpPost]
        public IActionResult Post([FromBody]ConfigurationRequest request)
        {
            var rs = _configurationsBusinessLogic.CreateConfiguration(request);
            return Ok(rs);
        }

        // PUT: api/Configuration/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]ConfigurationRequest request)
        {
            var rs = _configurationsBusinessLogic.EditConfiguration(request, id);
            return Ok(rs);
        }
    }
}
