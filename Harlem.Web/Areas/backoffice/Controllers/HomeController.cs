using Harlem.BLL.Abstract;
using Harlem.Entity.FrontEndTypes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harlem.Web.Areas.backofis.Controllers
{
    [Area("BackOffice")]
    [Authorize( Roles = "Admin")]
    public class HomeController : Controller
    {
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
            if (products.Status == Core.Tools.Enums.BLLResultType.Success)
            {
                ViewBag.Products = products.Entity;
            }

            return View();
        }
    }
}
