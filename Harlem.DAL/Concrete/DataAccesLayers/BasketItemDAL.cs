using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class BasketItemDAL : DataAccessTemplate<Context.HarlemContext, BasketItem>, IBasketItemDAL
    {
        public List<BasketItem> GetAllWithProduct(Expression<Func<BasketItem, bool>> condition = null)
        {
            
                using (Context.HarlemContext context = new Context.HarlemContext())
                {
                    return context.BasketItem
                         .Where(entity => entity.isDelete == false)
                         .Where(condition ?? (entity => true)).Include(x=> x.Product)
                         .Select(x => new BasketItem()
                         {
                             Id = x.Id,
                             Qty=x.Qty,
                             BasketId=x.BasketId,
                             Product=x.Product,
                             ProductId=x.ProductId
                         }).ToList();
                }
            
        }
    }
}
