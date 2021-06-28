using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class BasketItemManager : TemplateDataService<BasketItem,IBasketItemDAL>, IBasketItemService
    {
        public BasketItemManager(IBasketItemDAL basketItemDAL) : base(basketItemDAL)
        {
            this._dataProvider = basketItemDAL;
        }
    }
    
}
