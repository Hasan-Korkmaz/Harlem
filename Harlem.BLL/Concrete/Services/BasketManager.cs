using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class BasketManager : TemplateDataService<Basket, IBasketDAL>, IBasketService
    {
        public BasketManager(IBasketDAL basketDAL) : base(basketDAL)
        {
            this._dataProvider = basketDAL;
        }
    }
    
}
