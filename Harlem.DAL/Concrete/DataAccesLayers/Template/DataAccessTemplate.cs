using Harlem.DAL.Abstract.Template;
using Harlem.Entity.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Harlem.DAL.Concrete.DataAccesLayers.Template
{
    public class DataAccessTemplate<TRepository, TEntity> : IDataAccesTemplate<TEntity> where TRepository : DbContext, new() where TEntity : BaseEntity
    {
        public TEntity Add(TEntity entity)
        {
            using (TRepository Context = new TRepository())
            {
                Context.Attach(entity);
                var state = Context.Entry(entity);
                state.State = EntityState.Added;
                Context.SaveChanges();
                return entity;
            }
        }
        public TEntity Update(TEntity entity)
        {
            using (TRepository Context = new TRepository())
            {
                Context.Entry(entity).State = EntityState.Modified;
                 Context.SaveChanges();
                return entity;
            }
        }
        public bool Delete(TEntity entity)
        {
            using (TRepository Context = new TRepository())
            {
                var state = Context.Entry(entity);
                state.State = EntityState.Modified;
                 Context.SaveChanges();
                return true;
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> condition = null)
        {
            using (TRepository Context = new TRepository())
            {
            return Context.Set<TEntity>().Where(entity => entity.isDelete == false).Where(condition ?? (entity => true)).FirstOrDefault();
            }
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> condition = null)
        {
            using (TRepository Context = new TRepository())
            {
                return Context.Set<TEntity>().Where(entity => entity.isDelete == false)
                .Where(condition ?? (entity => true))
                .ToList();
            }
        }

    }
}
