using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineCassino.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().Where(criteria);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>() as IQueryable<T>;
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
