using Harlem.BLL.Abstract.Template;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Users;

namespace Harlem.BLL.Abstract
{
    public interface IUserService : IDataService<User>
    {
        UserDTO CheckUser(UserLoginDTO model);
    }
}
