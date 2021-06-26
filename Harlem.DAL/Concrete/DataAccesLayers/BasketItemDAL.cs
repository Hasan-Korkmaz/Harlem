using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class BasketItemDAL : DataAccessTemplate<Context.HarlemContext, BasketItem>, IBasketItemDAL { }
}
