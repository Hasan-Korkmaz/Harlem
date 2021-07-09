using Harlem.BLL.Abstract.Template;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Abstract
{
    public interface IOrderService : IDataService<Order>
    {
        public Result<List<Order>> GetAllOrder(Expression<Func<Order, bool>> condition = null);

    }
}
