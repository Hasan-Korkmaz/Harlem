using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Concrete.Services
{
    public class AccountUserAddressManager : TemplateDataService<AccountUserAddress, IAccountUserAddressDAL>,  IAccountUserAddressService
    {
        public AccountUserAddressManager(IAccountUserAddressDAL accountUserAddressDAL)
        {
            this._dataProvider = accountUserAddressDAL;
        }

        public Result<List<UserAddressDTO>> GetAllDTO(Expression<Func<AccountUserAddress, bool>> condition = null)
        {
            var result = new Result<List<UserAddressDTO>>();
            try
            {
                result.Entity = _dataProvider.GetAllDTO(condition);
                if (result.Entity != null)
                {
                    result.Status = Enums.BLLResultType.Success;
                    //MagicStringler kaldırılacak.
                    result.Message = "Eşleşen kayıt görüntüleniyor.";
                }
                else
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = "Eşleşen kayıt bulunamadı.";
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
