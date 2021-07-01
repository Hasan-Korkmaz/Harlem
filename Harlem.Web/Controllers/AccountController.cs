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
using System.Linq;
using System.Linq.Expressions;
using Harlem.Entity.DTO.Catalog;

namespace Harlem.Web.Controllers
{
    [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
    public class AccountController : _BaseController
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
            UserManager userManager) : base(userService)
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
            var user = userService.CheckUser(model);
            if (user != null && user.Role == "Customer")
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
                await this.HttpContext.SignInAsync("CustomerCookie", principal, authProperties);

                //Eğer Kullanıcı Oturum açmadan Sepete item eklediyse onları kullanıcının sepetine taşı
                if (HttpContext.Request.Cookies["SessionId"] != null)
                {
                    var sessionId = Guid.Parse(HttpContext.Request.Cookies["SessionId"]);
                    //Kullanıcının Sessiondaki Sepeti
                    var accountHaveBasketSessionBase = basketService.Get(x => x.SessionId == sessionId);
                    //Kullanıcının Normal Sepeti
                    var accountHaveBasketUserBase = basketService.Get(x => x.AccountUserId == user.Id && x.isCompleted == false);
                    if (accountHaveBasketSessionBase.Status == Enums.BLLResultType.Success)
                    {
                        var basketId = accountHaveBasketSessionBase.Entity.Id;
                        var basketItemsForSession = basketItemService.GetAllWithProduct(x => x.BasketId == basketId);
                        if (basketItemsForSession.Status == Enums.BLLResultType.Success)
                        {
                            Basket userBasket;
                            List<BasketItem> basketItemsForUser = new List<BasketItem>();
                            if (accountHaveBasketUserBase.Status == Enums.BLLResultType.Success)
                            {
                                userBasket = accountHaveBasketUserBase.Entity;
                                var basketItemBLL = basketItemService.GetAllWithProduct(x => x.Id == userBasket.Id);
                                if (basketItemBLL.Status == Enums.BLLResultType.Success)
                                {
                                    basketItemsForUser = basketItemBLL.Entity.ToList();
                                }
                                else
                                {
                                    basketItemsForUser = new List<BasketItem>();
                                }
                            }
                            else
                            {
                                userBasket = new Basket();
                                userBasket.Id = Guid.NewGuid();

                                userBasket.AccountUserId = user.Id;
                                userBasket.isActive = true;
                                userBasket.isCompleted = false;
                                userBasket.TotalPrice = 0;
                                basketService.Add(userBasket);
                            }
                            //Sessiondaki Sepeti Gez
                            foreach (var item in basketItemsForSession.Entity.ToList())
                            {
                                //Eğer Bu item kullanıcı Sepetinde yoksa ekle
                                if (basketItemsForUser.Where(x => x.ProductId == item.ProductId).FirstOrDefault() == null)
                                {
                                    basketItemService.Add(new BasketItem()
                                    {
                                        Id = Guid.NewGuid(),
                                        BasketId = userBasket.Id,
                                        isActive = true,
                                        Price = item.Product.Price,
                                        Qty = 1,
                                        ProductId = item.ProductId
                                    });
                                }
                                else
                                {
                                    //Eğer Bu item kullanıcı Sepetinde varsa sayısını +=1 yap
                                    var updateModel = basketItemService.Get(x => x.Id == item.Id).Entity;
                                    updateModel.Qty += 1;
                                    basketItemService.Update(updateModel);
                                }
                                basketItemService.Delete(item);
                            }
                            basketService.DeleteExpression(x => x.SessionId == sessionId);
                            var Cookieoption1 = new CookieOptions();
                            Cookieoption1.Path = HttpContext.Request.PathBase;
                            HttpContext.Response.Cookies.Delete("SessionId", Cookieoption1);
                        }
                    }
                }


                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { login = 1 });
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

        public IActionResult MyAddress()
        {
            var claimIdValue = this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            if (claimIdValue == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Guid id = Guid.Parse(claimIdValue);
            //Contexten alınacak
            var entity = accountUserAddressService.GetAll(x => x.isActive == true && x.UserId == id);
            if (entity.Status == Enums.BLLResultType.Success)
            {
                return View(entity.Entity);
            }
            return View();

        }
        [HttpGet]
        public IActionResult UpdateAddress(Guid id)
        {
            var entity = accountUserAddressService.Get(x => x.Id == id);
            if (entity.Status == Enums.BLLResultType.Success)
            {
                ViewBag.Title = "Adres Güncelleme";
                ViewBag.Link = "/Account/UpdateAddress";

                return View("AddressUpdateForm", entity.Entity);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult UpdateAddress(UserAddressDTO addressDTO)
        {
            var claimIdValue = this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            if (claimIdValue == null)
            {
                return RedirectToAction("Index", "home");
            }
            Guid id = Guid.Parse(claimIdValue);
            //Contexten alınacak
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyAddress", "Account");
            }
            var adres = accountUserAddressService.Get(x => x.isActive == true && x.Id == addressDTO.Id);
            if (adres.Status == Enums.BLLResultType.Success)
            {
                var entity = adres.Entity;
                entity.Name = addressDTO.Name;
                entity.AddressDetail = addressDTO.AddressDetail;
                var status = accountUserAddressService.Update(entity);
                if (status.Status == Enums.BLLResultType.Success)
                {
                    return RedirectToAction("MyAddress", "Account");

                }
                else
                {
                    return BadRequest();

                }

            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult NewAddress(AccountUserAddress address)
        {
            var claimIdValue = this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            if (claimIdValue == null)
            {
                return RedirectToAction("Index", "home");
            }
            Guid id = Guid.Parse(claimIdValue);
            //Contexten alınacak
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyAddress", "Account");
            }
            address.Id = Guid.NewGuid();
            address.isActive = true;
            address.UserId = id;
            var adres = accountUserAddressService.Add(address);
            if (adres.Status == Enums.BLLResultType.Success)
            {

                return RedirectToAction("MyAddress", "Account");
            }
            return BadRequest();
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
            return RedirectToAction("MyAddress", "Account");

        }

        public IActionResult NewAddress()
        {
            ViewBag.Title = "Adres Güncelleme";
            ViewBag.Link = "/Account/NewAddress";

            return View("AddressUpdateForm", new AccountUserAddress());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddBasket(Guid id)
        {

            Basket basket;
            var sessionId = Guid.Empty;
            var userId = Guid.Empty;
            var product = productService.Get(x => x.Id == id && x.isActive == true).Entity;
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //Kullanıcı Oturum Açmamış; Bi Kontrol Et Daha önce belli bir sepeti var mı diye ?
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
                Guid.TryParse(this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault(), out userId);
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
        public IActionResult GetBasket()
        {
            List<BasketItem> basketItems = new List<BasketItem>() ;
            Basket basket = new Basket();
            Guid userId =Guid.Empty;
            if (User.Identity.IsAuthenticated)
            {
                Guid.TryParse(this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault(), out userId);
            }
            if (userId!=Guid.Empty)
            {
              var basketStat=  basketService.Get(x => x.AccountUserId == userId);
                if (basketStat.Status==Enums.BLLResultType.Success)
                {
                    var q= basketItemService.GetAllWithProduct(x => x.BasketId == basketStat.Entity.Id);
                    if (q.Status==Enums.BLLResultType.Success)
                    {
                        decimal total = 0;
                        foreach (var item in q.Entity)
                        {
                            total=item.Price*item.Qty;
                        }
                        basketStat.Entity.TotalPrice = total;
                        basketService.Update(basketStat.Entity);
                        basketStat.Entity.BasketItem = basketItems;
                        basket = basketStat.Entity;
                    }
                }
            }
            var adress = accountUserAddressService.GetAll(x => x.UserId == userId);

            ViewBag.UserAddresses = adress;
            
            
            return View(basket);
        }
    }
}
