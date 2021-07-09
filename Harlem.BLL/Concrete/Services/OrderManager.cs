using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Concrete.Services
{
    public class OrderManager : TemplateDataService<Order, IOrderDAL>, IOrderService 
    {
        public OrderManager(IOrderDAL orderDAL) : base(orderDAL)
        {
        }
        public Result<List<Order>> GetAllOrder(Expression<Func<Order, bool>> condition = null)
        {
            var result = new Result<List<Order>>();
            try
            {
                result.Entity = _dataProvider.GetAllOrder(condition);

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
                    result.Message = " Eşleşen kayıt bulunamadı.";
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
