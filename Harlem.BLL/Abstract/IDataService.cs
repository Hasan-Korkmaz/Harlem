using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.BLL.Abstract
{
   public interface IDataService<TEntity> where TEntity : BaseEntity
    {
         Result<TEntity>  Add(TEntity entity);
         Result<TEntity> Update(TEntity entity);
         Result<TEntity> Delete(TEntity entity);
         Result<TEntity> DeleteExpression(Expression<Func<TEntity, bool>> condition);
         Result<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> condition = null);
         Result<TEntity> Get(Expression<Func<TEntity, bool>> condition = null);
    }
}
