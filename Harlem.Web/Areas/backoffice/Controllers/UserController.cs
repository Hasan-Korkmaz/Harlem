using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        public IActionResult Index()
        {
            ViewBag.ActiveMenu.ActiveSubMenu = "user";
            ViewBag.ActiveMenu.ActiveTopMenu = "defination";
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Insert;
            return View("UserInsertOrUpdateForm");
        }

        public IActionResult Update(Guid id)
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Update;
            return View("");
        }

        [HttpPost]
        public ApiResponse<User> Add(User user)
        {

            return null;
        }

        [HttpPost]
        public ApiResponse<User> Update(User user)
        {

            return null;
        }

        [HttpPut]
        public ApiResponse<User> Delete(Guid id)
        {
            throw new Exception();
        }

        [HttpPost]
        public ApiResponse<User> GetList()
        {

            return null;
        }
    } 
}
