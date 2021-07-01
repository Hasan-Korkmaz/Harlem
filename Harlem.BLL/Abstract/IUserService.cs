using Harlem.BLL.Abstract.Template;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Users;
using System;
using System.Linq.Expressions;

namespace Harlem.BLL.Abstract
{
    public interface IUserService : IDataService<User>
    {
        UserDTO CheckUser(UserLoginDTO model);
        UserDTO GetUserWithRoleQuery(Expression<Func<AccountUser, bool>> condition = null);
    }
}
