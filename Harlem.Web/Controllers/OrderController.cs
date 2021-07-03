using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Harlem.Web.Controllers
{
    [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]

    public class OrderController : _BaseController
    {
        IUserService userService;
        IBasketService basketService;
        IOrderService orderService;
        IProductService productService;
        public OrderController(IUserService userService,
                               IBasketService basketService,
                               IOrderService orderService, IProductService productService) : base(userService)
        {
            this.userService = userService;
            this.basketService = basketService;
            this.orderService = orderService;
            this.productService = productService;
        }
        [Authorize(Roles = "Customer", AuthenticationSchemes = "CustomerCookie")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Complete(StartOrder order)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            Dictionary<Guid, int> stockUpdateProducts = new Dictionary<Guid, int>();
            Guid userId = Guid.Empty;
            Result<Order> orderStat=new Result<Order>();
            Guid.TryParse(this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault(), out userId);

            var basketResult =  basketService.Get(x => x.Id == order.BasketId && x.isCompleted==false);
            decimal total = 0;
            if (basketResult.Status==Core.Tools.Enums.BLLResultType.Success)
            {
                //Sepettekileri Siparişe Taşıyorum
                foreach (var item in basketResult.Entity.BasketItem)
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Price = item.Product.Price,
                        Qty = item.Qty,
                        ProductId = item.ProductId,
                        isActive = true,
                    });
                    total += item.Product.Price * item.Qty;
                    stockUpdateProducts.Add(item.ProductId, item.Qty);
                }
                //Sepet yerine Order oluşturdum ve taşıdım
                orderStat = orderService.Add(new Entity.DbModels.Order()
                {
                    Id = Guid.NewGuid(),
                    AccountUserId=userId,
                    isActive=true,
                    PaymentType=order.PaymentType,
                    AccountUserAddressId= order.AddressId,
                    TotalPrice=total,
                    isDelivered=false,
                    OrderItem=orderItems,
                    OrderDate=DateTime.Now,
                });
                //Siparişi geçilen ürünleri stoktan düştüm
                foreach (var item in stockUpdateProducts)
                {
                    var product = productService.Get(x => x.Id == item.Key).Entity;
                    product.StockPiece -= item.Value;
                    productService.Update(product);
                }
                //Sepeti Tamamlanmış olarak işaretledim
                basketResult.Entity.isCompleted = true;
                basketService.Update(basketResult.Entity);
            }
            if (orderStat.Status==Enums.BLLResultType.Success)
            {
                return View(new { Status=true,Message="SİPARİŞİNİZ BAŞARILI BİR ŞEKİLDE ALINMIŞTIR."});
            }
            return View(new { Status = false, Message = "SİPARİŞ ALINIRKEN BİR HATA OLUŞTU" });

        }
    }
}
