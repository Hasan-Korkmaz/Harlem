using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Harlem.Entity.DTO.Catalog;
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
                         StockType = x.StockType,
                         ProductDetail = x.ProductDetail,

                     }).ToList();
            }
        }
        public List<ProductDTO> GetWithProductImages(Expression<Func<Product, bool>> condition = null)
        {
            using (Context.HarlemContext context = new Context.HarlemContext())
            {


                var product =  context.Products
                     .Where(entity => entity.isDelete == false)
                     .Where(condition ?? (entity => true)).Select( x=> new ProductDTO() {
                     Id=x.Id,
                     Name=x.Name,
                     Price=x.Price,
                     StockPiece=x.StockPiece,
                     CategoryName=x.Category.DisplayName,
                     StockType=x.StockType,
                     ProductImages=x.ProductImages.Select(y=> new ProductImageDTO() {Id=y.Id,ImageName=y.ImageName,Order=y.Order,ImageAltValue=y.ImageAltValue,PublicURL=y.PublicURL }).ToList()})
                     .ToList();

             
                return product;


            }
        }

    }
}
