using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO.Users;

namespace Harlem.BLL.Concrete.Services
{
    public class UserManager : TemplateDataService<User, IUserDAL>, IUserService
    {
        private readonly IAccountUserDAL accountUserDAL;
        private readonly IUserDAL userDAL;

        public UserManager(IUserDAL userDAL, IAccountUserDAL accountUserDAL)
            : base(userDAL)
        {
            this.accountUserDAL = accountUserDAL;
            this.userDAL = userDAL;
        }

        public UserDTO CheckUser(UserLoginDTO model)
        {

            var data = _dataProvider.Get(x => x.Email == model.Email && x.Password == Extensions.Md5Hash(model.Password));
            if (data != null)
                return new UserDTO
                {
                    Id = data.Id,
                    Role = "Admin",
                    Email = data.Email,
                    Name = data.Name,
                    Surname = data.Surname
                };
            else if (data == null)
            {
                var accountUser = accountUserDAL.Get(x => x.Email == model.Email && x.Password == Extensions.Md5Hash(model.Password));
                if (accountUser != null)
                {
                    return new UserDTO
                    {
                        Id = accountUser.Id,
                        Role = "Customer",
                        Email = accountUser.Email,
                        Name = accountUser.Name,
                        Surname = accountUser.Surname
                    };
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
