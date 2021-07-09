using Harlem.BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Harlem.Entity.DTO.Users;

namespace Harlem.Web.Controllers
{
    public abstract class _BaseController : Controller
    {
        IUserService userService;
         internal UserDTO User { get; set; }
        public _BaseController(IUserService userService)
        {
            this.userService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("Customer"))
            {
                Guid claim;
                Guid.TryParse( this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault(),out claim);
                var userItem=userService.GetUserWithRoleQuery(x => x.Id == claim);
                if (userItem!=null)
                {
                    userItem.FullName = ((userItem.Name + " " + userItem.Surname).Length < 30 ? (userItem.Name + " " + userItem.Surname).ToString() : (userItem.Name + " " + userItem.Surname).Substring(0,30) + ".");
                    ViewBag.ActiveUser = userItem;
                    User = userItem;
                }
          
            }
            base.OnActionExecuting(filterContext);
        }
    }

}
