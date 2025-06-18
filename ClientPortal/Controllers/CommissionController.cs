using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers
{
    public class CommissionController : Controller
    {
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
