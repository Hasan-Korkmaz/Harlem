using Harlem.BLL.Abstract;
using Harlem.Entity.FrontEndTypes;
using Harlem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Controllers
{
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
        public IActionResult Index()
        {
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
