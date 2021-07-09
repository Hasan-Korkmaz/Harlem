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
    public class OrderDAL : DataAccessTemplate<Context.HarlemContext, Order>, IOrderDAL {

       
        public List<Order> GetAllOrder(Expression<Func<Order, bool>> condition = null)
        {
            using (Context.HarlemContext context = new Context.HarlemContext())
            {
                return context.Orders
                     .Where(entity => entity.isDelete == false)
                     .Where(condition ?? (entity => true))
                     .Include(x=> x.Address)
                     .Include(x=> x.OrderItem)
                     .ThenInclude(x=> x.Product)
                     .ToList();
            }
        }

    }
}
