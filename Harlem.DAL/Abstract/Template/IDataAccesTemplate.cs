using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.DAL.Abstract.Template
{
   public interface IDataAccesTemplate<TEntity> where TEntity : BaseEntity
    {
       
            TEntity Add(TEntity entity);
            TEntity Update(TEntity entity);
            bool Delete(TEntity entity);
            List<TEntity> GetAll(Expression<Func<TEntity, bool>> condition = null);
            TEntity Get(Expression<Func<TEntity, bool>> condition = null);
        
    }
}
