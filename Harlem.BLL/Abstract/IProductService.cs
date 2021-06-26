using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.BLL.Abstract
{
   public interface IProductService: IDataService<Product>
    {
        public Result<List<ProductDTO>> GetAllDTO(Expression<Func<Product, bool>> condition = null);
        public Result<List<Product>> GetWithProductImages(Expression<Func<Product, bool>> condition = null);

    }
}
