using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IRepositoy<TEntity> where TEntity : class
    {
        bool IsError { get; set; }

        TEntity Get(int? ID);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

    public class Repository<TEntity> : IRepositoy<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public bool IsError { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int? ID)
        {
            return Context.Set<TEntity>().Find(ID);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
            }

        }

        public void Update(TEntity entity)
        {

            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();

            }
            catch (Exception)
            {
                IsError = true;
            }

        }

        public void Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
            }
        }
    }
}
