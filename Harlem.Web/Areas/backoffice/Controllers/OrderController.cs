using Harlem.BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CancelOrder(int orderId)
        {

            return Ok();
        }
    }
}
