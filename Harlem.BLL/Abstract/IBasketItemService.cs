using Harlem.BLL.Abstract.Template;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Abstract
{
    public interface IBasketItemService : IDataService<BasketItem>
    {
        public Result<List<BasketItem>> GetAllWithProduct(Expression<Func<BasketItem, bool>> condition = null);

    }
}
