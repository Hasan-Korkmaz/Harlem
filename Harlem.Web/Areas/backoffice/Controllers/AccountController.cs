using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string userName,string password)
        {
            Response.Redirect("\\Backoffice\\Product\\Index");
        }
    }
}
