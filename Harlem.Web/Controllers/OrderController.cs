using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        IOrderItemService orderItemService;
        public OrderController(IUserService userService,
                               IBasketService basketService,
                               IOrderService orderService, 
                               IProductService productService,
                               IOrderItemService orderItemService) : base(userService)
        {
            this.userService = userService;
            this.basketService = basketService;
            this.orderService = orderService;
            this.productService = productService;
            this.orderItemService = orderItemService;
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
            Result<Order> orderStat=new Result<Order>();
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
                    AccountUserId=User.Id,
                    isActive=true,
                    PaymentType=order.PaymentType,
                    AccountUserAddressId= order.AddressId,
                    TotalPrice=total,
                    isDelivered=false,
                    OrderItem=orderItems,
                    OrderDate=DateTime.Now,
                });
                foreach (var item in orderItems)
                {
                   item.OrderId=orderStat.Entity.Id;
                    orderItemService.Add(item);
                }
               
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
                return View(new Entity.FrontEndTypes.StatusMessage() { Status=true,Message="SİPARİŞİNİZ BAŞARILI BİR ŞEKİLDE ALINMIŞTIR."});
            }
            return View(new Entity.FrontEndTypes.StatusMessage() { Status = false, Message = "SİPARİŞ ALINIRKEN BİR HATA OLUŞTU" });

        }

        public IActionResult GetOrders()
        {
         var orderResponse=   orderService.GetAllOrder(x => x.AccountUserId == User.Id);
            if (orderResponse.Status==Enums.BLLResultType.Success)
            {
                return View(orderResponse.Entity);

            }
            else
            {
                return RedirectToAction("InternalServerError", "Error");
            }

        }
    }
}
