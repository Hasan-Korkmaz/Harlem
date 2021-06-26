using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.DAL.Abstract
{
    public interface IProductDAL : Template.IDataAccesTemplate<Product>
    {
        List<ProductDTO> GetAllDTO(Expression<Func<Product, bool>> condition = null);
        public  List<Product> GetWithProductImages(Expression<Func<Product, bool>> condition = null);
    }
}
