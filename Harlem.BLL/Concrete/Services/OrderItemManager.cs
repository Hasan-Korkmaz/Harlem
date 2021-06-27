using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class OrderItemManager : TemplateDataService<OrderItem, IOrderItemDAL>, IOrderItemService 
    {
        public OrderItemManager(IOrderItemDAL orderItemDAL)
        {
            this._dataProvider = orderItemDAL;
        }
    }
    
}
