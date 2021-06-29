using Harlem.BLL.Abstract;
using Harlem.Entity.FrontEndTypes;
using Harlem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Harlem.Web.Controllers
{
    [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IProductService productService;
        ICategoryService categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
        [AllowAnonymous]
        public IActionResult Index(string ReturnUrl, int login = 0)
        {
            ViewBag.LoginOpen = false;
            if (login == 1)
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
