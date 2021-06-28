using Harlem.BLL.Abstract.Template;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Abstract
{
    public interface IAccountUserAddressService : IDataService<AccountUserAddress>
    {
        public Result<List<UserAddressDTO>> GetAllDTO(Expression<Func<AccountUserAddress, bool>> condition = null);
    }
}
