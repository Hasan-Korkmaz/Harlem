using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class AccountUserAddressDAL : DataAccessTemplate<Context.HarlemContext, AccountUserAddress>, IAccountUserAddressDAL
    {
      
        public List<UserAddressDTO> GetAllDTO(Expression<Func<AccountUserAddress, bool>> condition = null)
        {
            using (Harlem.DAL.Concrete.Context.HarlemContext Context = new Context.HarlemContext())
            {
                return Context.Set<AccountUserAddress>().Where(entity => entity.isDelete == false)
                .Where(condition ?? (entity => true)).Select(x=> new UserAddressDTO()
                {
                    Id=x.Id,
                    AddressDetail=x.AddressDetail,
                    Name=x.Name,
                    UserId=x.UserId
                })
                .ToList();
            }
        }
    }
}
