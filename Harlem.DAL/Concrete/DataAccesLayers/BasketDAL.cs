using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class BasketDAL: DataAccessTemplate<Context.HarlemContext, Basket>, IBasketDAL {
        public override Basket Get(Expression<Func<Basket, bool>> condition = null)
        {
            using (Context.HarlemContext context = new Context.HarlemContext())
            {
                return context.Basket
                     .Where(entity => entity.isDelete == false)
                     .Where(condition ?? (entity => true)).Include(x=> x.BasketItem).ThenInclude(x=> x.Product).FirstOrDefault();
            }
        }
    }

}
