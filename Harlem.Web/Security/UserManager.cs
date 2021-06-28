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

        public async Task<bool> SingIn(HttpContext httpContext, UserLoginDTO model, bool isPersistent = false)
        {
            var user = userService.CheckUser(model);

            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    GetUserClaims(user),
                    CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }


        private IEnumerable<Claim> GetUserClaims(UserDTO user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            claims.AddRange(this.GetUserRoleClaims(user));

            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(UserDTO user)
        {

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            return claims;
        }
    }
}
