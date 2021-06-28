using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;

namespace Harlem.BLL.Concrete.Services
{
    public class CategoryManager: TemplateDataService<Category, ICategoryDAL>,ICategoryService
    {
        public CategoryManager(ICategoryDAL categoryDAL) : base(categoryDAL)
        {
           
        }
    }
}
