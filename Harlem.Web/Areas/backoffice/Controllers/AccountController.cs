using Harlem.Entity.DTO.Users;
using Harlem.Web.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class AccountController : Controller
    {
        private readonly UserManager userManager;

        public AccountController(UserManager userManager)
        {
            this.userManager = userManager;
        }

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
        public async Task<IActionResult> Login(UserLoginDTO login)
        {
            await userManager.SingIn(this.HttpContext, login);

            if (this.HttpContext.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "backoffice" });
            }
            else if (this.HttpContext.User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}
