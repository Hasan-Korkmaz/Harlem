using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.DAL.Abstract
{
    public interface IBasketItemDAL : Template.IDataAccesTemplate<BasketItem> {
        List<BasketItem> GetAllWithProduct(Expression<Func<BasketItem, bool>> condition = null);

    }
}
