using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class OrderItemDAL : DataAccessTemplate<Context.HarlemContext, OrderItem>, IOrderItemDAL { }
}
