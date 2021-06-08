using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class ProductDAL : DataAccessTemplate<Context.HarlemContext, Product>, IProductDAL
    {
        public List<ProductDTO> GetAllDTO(Expression<Func<Product, bool>> condition = null)
        {
            using (Context.HarlemContext context = new Context.HarlemContext())
            {
                return context.Products
                     .Where(entity => entity.isDelete == false)
                     .Where(condition ?? (entity => true))
                     .Select(x => new ProductDTO()
                     {
                         Id=x.Id,
                         CategoryName = x.Category.DisplayName,
                         isActive = x.isActive,
                         Name = x.Name,
                         Price = x.Price,
                         StockPiece = x.StockPiece,
                         StockType = x.StockType.ToString(),
                         ProductDetail = x.ProductDetail,

                     }).ToList();
            }
        }
    }
}
