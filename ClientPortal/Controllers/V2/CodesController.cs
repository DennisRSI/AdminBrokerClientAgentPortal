using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codes.Service.Interfaces.V2;
using Codes.Service.ViewModels.V2;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientPortal.Controllers.V2
{
    [Route("api/v2/[controller]")]
    public class CodesController : Controller
    {
        private readonly ICodesService _codeService;

        public CodesController(ICodesService codeService)
        {
            _codeService = codeService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ListViewModel<BrokerClientAgentViewModel>> Get(SearchViewModel search)
        {
            ListViewModel<BrokerClientAgentViewModel> model = new ListViewModel<BrokerClientAgentViewModel>();

            try
            {
                model = await _codeService.BrokerClientAgentInfo(search);
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ListViewModel<BrokerClientAgentViewModel>();

                model.IsSuccess = false;
                model.Message = ex.Message;
            }

            return model;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
