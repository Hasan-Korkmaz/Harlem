using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Catalog;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.DAL.Abstract
{
    public interface IAccountUserAddressDAL : Template.IDataAccesTemplate<AccountUserAddress> {
        List<UserAddressDTO> GetAllDTO(Expression<System.Func<AccountUserAddress, bool>> condition = null);

    }
}
