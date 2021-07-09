using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.DAL.Abstract
{
    public interface IOrderDAL : Template.IDataAccesTemplate<Order>
    {
        public List<Order> GetAllOrder(Expression<Func<Order, bool>> condition = null);

    }
}
