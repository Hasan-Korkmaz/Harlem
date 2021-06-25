using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class UserManager : TemplateDataService<User, IUserDAL>, IUserService
    {

    }
}
