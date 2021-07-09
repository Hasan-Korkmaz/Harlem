using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult InternalServerError()
        {
            return View();
        }
    }
}
