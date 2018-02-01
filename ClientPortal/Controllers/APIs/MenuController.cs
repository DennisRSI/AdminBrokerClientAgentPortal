using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MenuController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{cmd?}/{msg?}")]
        public IActionResult Get(string id, string cmd = "", string msg = "")
        { 
            switch (id)
            {
                case "admin-change":
                    switch (cmd)
                    {
                        case "broker":
                            return null;
                        case "agent":
                            return null;
                        default:
                            return null;
                    }
                case "user-list":
                    return ViewComponent("UserManagement", new { type = cmd });
                case "client-details":
                    return ViewComponent("ClientDetails", new { accountId = cmd });
                case "clients-campaigns":
                    return null;
                case "campaign-list":
                    return null;
                case "my-account":
                    return ViewComponent("MyAccount", new { accountId = cmd, message = msg });
                default:
                    return ViewComponent("Dashboard");


            }
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
