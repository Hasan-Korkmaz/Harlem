using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Concrete.Services
{
    public class BasketItemManager : TemplateDataService<BasketItem,IBasketItemDAL>, IBasketItemService
    {
        public BasketItemManager(IBasketItemDAL basketItemDAL) : base(basketItemDAL)
        {
            this._dataProvider = basketItemDAL;
        }

        public Result<List<BasketItem>> GetAllWithProduct(Expression<Func<BasketItem, bool>> condition = null)
        {
            var result = new Result<List<BasketItem>>();
            try
            {
                result.Entity = _dataProvider.GetAllWithProduct(condition);

                if (result.Entity.Count > 0)
                {
                    result.Status = Enums.BLLResultType.Success;
                    //MagicStringler kaldırılacak.
                    result.Message = " Eşleşen " + result.Entity.Count + "  kayıt görüntüleniyor.";
                }
                else if (result.Entity.Count == 0)
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = result.Entity.Count+ " Eşleşen kayıt bulunamadı.";
                }
                else
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = "Arama yapılırken bir hata oluştu.";
                }

            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt arama işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";

            }
            return result;
        }
    }
    
}
