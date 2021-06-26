using Microsoft.AspNetCore.Mvc;

namespace Harlem.Web.Areas.backofis.Controllers
{
    [Area("BackOffice")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
