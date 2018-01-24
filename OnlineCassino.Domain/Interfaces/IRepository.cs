using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineCassino.Domain.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> criteria);
        void Add(T entity);
        void Remove(T entity);
    }
}
