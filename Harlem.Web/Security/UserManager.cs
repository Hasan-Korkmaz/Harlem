using Harlem.BLL.Abstract;
using Harlem.Entity.DTO.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Harlem.Web.Security
{
    public class UserManager
    {
        private readonly IUserService userService;

        public UserManager(IUserService userService)
        {
            this.userService = userService;
        }


        
       

        

       
    }
}
