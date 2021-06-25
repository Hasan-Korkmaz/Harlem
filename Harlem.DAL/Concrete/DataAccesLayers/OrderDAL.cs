using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class OrderDAL : DataAccessTemplate<Context.HarlemContext, Order>, IOrderDAL { }
}
