using Microsoft.AspNetCore.Mvc;

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
