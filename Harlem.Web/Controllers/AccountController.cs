using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Users;
using Harlem.Entity.FrontEndTypes.ViewModels;
using Harlem.Web.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Harlem.Web.Controllers
{
    [Authorize(Roles = "Customer",AuthenticationSchemes ="CustomerCookie")]
    public class AccountController : Controller
    {
        IAccountUserService accountUserService;
        IBasketService basketService;
        IBasketItemService basketItemService;
        IProductService productService;
        IAccountUserAddressService accountUserAddressService;
        IUserService userService;
        private readonly UserManager userManager;

        public AccountController(
            IAccountUserService accountUserService, 
            IBasketService basketService, 
            IBasketItemService basketItemService, 
            IProductService productService, 
            IAccountUserAddressService accountUserAddressService,
            IUserService userService,
            UserManager userManager)
        {
            this.accountUserService = accountUserService;
            this.basketService = basketService;
            this.basketItemService = basketItemService;
            this.productService = productService;
            this.accountUserAddressService = accountUserAddressService;
            this.userManager = userManager;
            this.userService = userService;
        }
     

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            var user= userService.CheckUser(model);
            if (user != null && user.Role=="Customer")
            {
              
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddYears(1),
                    IsPersistent = model.RememberMe,
                };
                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomerCookie"));
                    await this.HttpContext.SignInAsync("CustomerCookie", principal,authProperties);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index","Home",new {login=1 });
            }


        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddAccountUser(AccountUserRegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new AccountUser();
                var data = accountUserService.Get(x => x.Email == registerModel.Mail);
                if (data.Status == Enums.BLLResultType.Success)
                {
                    ViewBag.AccountStatus = false;
                    ViewBag.AccountStatusMessage = "Yeni hesap açma işlemi oluşturulurken bir hata gerçekleşti.<br> <u style='color:red;'>ZATEN BU MAİL'E KAYITLI BİR HESAP VAR</u><br> Yeni yerine giriş yapın. <br> Eğer şifrenizi unuttuysanız yeni bir şifre talep edebilirsiniz.";
                    return View();
                }
                newUser.Name = registerModel.Name;
                newUser.Surname = registerModel.Surname;

                newUser.Email = registerModel.Mail.ToLower();
                newUser.Password = registerModel.Password.Md5Hash();
                newUser.Phone = registerModel.Phone;
                newUser.InsertDateTime = DateTime.Now;
                newUser.isActive = true;
                newUser.isDelete = false;
                newUser.Id = Guid.NewGuid();
                var result = accountUserService.Add(newUser);
                if (result.Status == Enums.BLLResultType.Success)
                {
                    ViewBag.AccountStatus = true;
                    ViewBag.AccountStatusMessage = "Yeni hesap açma işlemi başarılı bir şekilde gerçekleştirildi.<br> Artık Giriş yapabilirsiniz.";
                    return View();
                }
                else
                {
                    ViewBag.AccountStatus = false;
                    ViewBag.AccountStatusMessage = "Yeni hesap açma işlemi oluşturulurken bir hata gerçekleşti.<br> Şuan yeni kayıt oluşturamıyoruz.<br> Lütfen daha sonra tekrar deneyiniz.";
                    return View();
                }
            }
            else
            {

                ViewBag.AccountStatus = false;
                ViewBag.AccountStatusMessage = "Yeni hesap açma işlemi oluşturulurken bir hata gerçekleşti.<br> Girdiğiniz değerler eksik yada yanlış <br> Lütfen daha sonra tekrar deneyiniz.";
                return View();

            }



        }
        //Yalnızca Auth olduysa

        public IActionResult MyAdress(Guid id)
        {
            //Contexten alınacak
            var entity = accountUserAddressService.GetAll(x => x.isActive == true && x.UserId == id);
            if (entity.Status == Enums.BLLResultType.Success)
            {
                return View(entity.Entity);

            }
            return View();

        }

        public IActionResult DeleteAdress(Guid id)
        {
            //Contexten alınacak
            var entity1 = accountUserAddressService.DeleteExpression(x => x.Id == id);
            var entity = accountUserAddressService.GetAll(x => x.isActive == true && x.UserId == id);

            if (entity.Status == Enums.BLLResultType.Success)
            {
                return View(entity.Entity);

            }
            return View();

        }

        public IActionResult NewAdress()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddBasket(Guid id)
        {
            Basket basket;
            var sessionId = Guid.Empty;
            var userId = Guid.Empty;
            var product = productService.Get(x => x.Id == id).Entity;
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                if (HttpContext.Request.Cookies["SessionId"] != null)
                {
                    //Kullanıcı Oturum Açmamış Sepete Eklemiş ise 
                    sessionId = Guid.Parse(HttpContext.Request.Cookies["SessionId"]);
                }
                else
                {
                    sessionId = Guid.NewGuid();
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Append("SessionId", sessionId.ToString(), option);
                }
            }
            else
            {
                //Kullanıcı Oturum Açtıysa
                //UserId Set et
            }





            if (userId != Guid.Empty)
            {
                //KULLANICININ SEPETİ VARMI ?
                var accountHaveBasket = basketService.Get(x => x.AccountUserId == userId);
                if (accountHaveBasket.Status == Enums.BLLResultType.Success)
                {
                    basket = accountHaveBasket.Entity;
                }
                else
                {
                    basket = basketService.Add(new Entity.DbModels.Basket()
                    {
                        Id = Guid.NewGuid(),
                        AccountUserId = userId,
                        isActive = true,
                        isDelete = false,
                        isCompleted = false,
                        InsertDateTime = DateTime.Now
                    }).Entity;
                }
            }
            else
            {
                //KULLANICININ SEPETİ VARMI ?
                var accountHaveBasket = basketService.Get(x => x.SessionId == sessionId);
                if (accountHaveBasket.Status == Enums.BLLResultType.Success)
                {
                    basket = accountHaveBasket.Entity;
                }
                else
                {
                    basket = basketService.Add(new Entity.DbModels.Basket()
                    {
                        Id = Guid.NewGuid(),
                        SessionId = sessionId,
                        isActive = true,
                        isDelete = false,
                        isCompleted = false,
                        InsertDateTime = DateTime.Now
                    }).Entity;
                }
            }

            var basketItem = basketItemService.Get(x => x.ProductId == id);
            if (basketItem.Status == Enums.BLLResultType.Success)
            {
                basketItem.Entity.Qty++;
                basketItemService.Update(basketItem.Entity);
            }
            else
            {
                var basketItemNew = basketItemService.Add(new BasketItem()
                {
                    Id = Guid.NewGuid(),
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Price = product.Price,
                    ProductId = product.Id,
                    BasketId = basket.Id,
                    Qty = 1

                });
            }


            return View("Basket");
        }
        public IActionResult Basket()
        {
            return View();
        }
    }
}
