using Harlem.BLL.Abstract;
using Harlem.Entity.FrontEndTypes;
using Harlem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Harlem.Web.Controllers
{
    [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
    public class HomeController : _BaseController
    {
        private readonly ILogger<HomeController> _logger;
        IProductService productService;
        ICategoryService categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService,IUserService userService):base(userService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        //İkisini birden kullanmak lazım.
        //Anasayfada login olduysa  AUTHORiZE olmadan Claim'ini alamıyorum.
        //Bu yüzden hem izin veriyorum hemde izin vermiyorum ✌✌
        [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
        [AllowAnonymous]
        public IActionResult Index(string ReturnUrl)
        {
            
            ViewBag.LoginOpen = false;
            if (ReturnUrl != null)
            {
                ViewBag.LoginOpen = true;
            }
            ViewBag.ActiveMenu = new ActiveMenu { ActiveTopMenu = "dashboard" };
            var products = productService.GetWithProductImages(x => x.isActive == true);
            var category = categoryService.GetAll(x => x.isActive == true);
            if (products.Status == Core.Tools.Enums.BLLResultType.Success && category.Status == Core.Tools.Enums.BLLResultType.Success)
            {
                ViewBag.Products = products.Entity;
                ViewBag.Categories = category.Entity;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
