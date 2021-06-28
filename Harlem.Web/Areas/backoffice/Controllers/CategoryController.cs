using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewBag.ActiveMenu.ActiveSubMenu = "category";
            ViewBag.ActiveMenu.ActiveTopMenu = "defination";
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Insert;
            return View("CategoryInsertUpdateForm");
        }

        public IActionResult Update(Guid id)
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Update;
            var model = categoryService.Get(x => x.Id == id);
            if (model.Status != Enums.BLLResultType.Success)
            {

            }
            else
            {

            }
            return View("CategoryInsertUpdateForm", model.Entity);
        }

        [HttpPost]
        public ApiResponse<Category> Add(Category category)
        {
            var resp = categoryService.Add(category);
            return new ApiResponse<Category>() { Data = null, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }

        [HttpPost]
        public ApiResponse<Category> Update(Category category)
        {
            var resp = categoryService.Update(category);
            return new ApiResponse<Category>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }

        [HttpPut]
        public ApiResponse<Category> Delete(Guid Id)
        {
            var resp = categoryService.DeleteExpression(x => x.Id == Id);
            return new ApiResponse<Category>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }

        public ApiResponse<List<Category>> GetList()
        {
            //Yalnızca Kategori Tablosundaki verileri getirir dile bakmaz.
            var model = categoryService.GetAll();
            return new ApiResponse<List<Category>>() { Data = model.Entity, Message = model.Message, Status = model.Status == Enums.BLLResultType.Success ? true : false };


        }
    }
}
