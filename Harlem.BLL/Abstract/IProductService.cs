using Harlem.BLL.Abstract.Template;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Abstract
{
    public interface IProductService: IDataService<Product>
    {
        Result<List<ProductDTO>> GetAllDTO(Expression<Func<Product, bool>> condition = null);
        public Result<List<Product>> GetWithProductImages(Expression<Func<Product, bool>> condition = null);
    }
}
