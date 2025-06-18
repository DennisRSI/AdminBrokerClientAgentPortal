using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : Controller
    {
        //[Bind("startDate, endDate, startRowIndex, numberOfRows, isPaid, bookingType, brokerId")]BrokerAdminFilter model

        [HttpGet("broker")]
        public IActionResult Broker()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
