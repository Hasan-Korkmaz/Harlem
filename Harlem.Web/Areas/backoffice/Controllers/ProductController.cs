using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]

    public class ProductController : Controller
    {
        IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index()
        {
            ViewBag.ActiveSubMenu = "product";
            ViewBag.ActiveTopMenu = "defination";
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Insert;
            return View("ProductInsertUpdateForm");
        }
        public IActionResult Update(Guid id)
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Update;
            var model = productService.Get(x => x.Id == id);
            if (model.Status != Enums.BLLResultType.Success)
            {

            }
            else
            {

            }
            return  View("CategoryInsertUpdateForm", model.Entity);
        }

        [HttpPost]
        public ApiResponse<Product> Add( Product product)
        {
            var resp = productService.Add(product);
            return new ApiResponse<Product>() { Data = null, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPost]
        public ApiResponse<Product> Update(Product product)
        {
            var resp = productService.Update(product);
            return new ApiResponse<Product>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPut]
        public ApiResponse<Product> Delete(Guid Id)
        {
            var resp = productService.DeleteExpression(x => x.Id == Id);
            return new ApiResponse<Product>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }

        public ApiResponse<List<ProductDTO>> GetList()
        {
            //Yalnızca Kategori Tablosundaki verileri getirir dile bakmaz.
            var model = productService.GetAllDTO();
            return new ApiResponse<List<ProductDTO>>() { Data = model.Entity, Message = model.Message, Status = model.Status == Enums.BLLResultType.Success ? true : false };
        }
    }
}
