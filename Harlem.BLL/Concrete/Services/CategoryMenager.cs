using Harlem.BLL.Abstract;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.BLL.Concrete.Services
{
   public class CategoryMenager: TemplateDataService<Category, ICategoryDAL>,ICategoryService
    {
        public CategoryMenager(ICategoryDAL categoryDAL)
        {
            this._dataProvider = categoryDAL;
        }
    }
}
