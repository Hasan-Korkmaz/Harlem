using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.BLL.Concrete.Services
{
   
    public class ProductImageMenager : TemplateDataService<ProductImage, IProductImageDAL>, IProductImageService
    {
        public ProductImageMenager(IProductImageDAL productImageDal)
        {
            this._dataProvider = productImageDal;
        }

    }
}
