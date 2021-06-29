using Harlem.BLL.Abstract;
using Harlem.Entity.DTO.Users;
using Harlem.Web.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class AccountController : Controller
    {
        private readonly UserManager userManager;
        IUserService userService;

        public AccountController(UserManager userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int errorParam = 0)
        {
            var isAuth = HttpContext.User.IsInRole("Admin");

            if (!isAuth)
            {
                if (errorParam == 1)
                    ViewBag.AuthMessage = "Kullanıcı Adınız Şifreniz ile Eşleşmiyor.";

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "backoffice" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO login)
        {
            var user = userService.CheckUser(login);
            if (user != null && user.Role == "Admin")
            {

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddYears(1),
                    IsPersistent = login.RememberMe,
                };

                ClaimsIdentity identity = new ClaimsIdentity(
                        claims,
                        "BackofficeCookie");

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await this.HttpContext.SignInAsync("BackofficeCookie", principal, authProperties);

                return RedirectToAction("Index", "Home", new { area = "backoffice" });
            }
            else
            {
                return RedirectToAction("Login", "Account", new { errorParam = 1, area = "backoffice", });
            }

        }
    }
}
