using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase
{
    public interface IServiceBase<T> where T : class ,new()
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> IncludeSubSets(params Expression<Func<T, object>>[] includeProperties);
    }
}