using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class OrderManager : TemplateDataService<Order, IOrderDAL>, IOrderService 
    {
        public OrderManager(IOrderDAL orderDAL) : base(orderDAL)
        {
        }
    }
    
}
