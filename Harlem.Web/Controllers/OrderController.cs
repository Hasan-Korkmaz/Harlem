using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Controllers
{
    public class OrderController:Controller /*: _BaseController*/
    {
        [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
