using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepoT.RavenDb.MvcSample.SampleBase
{
    public interface IServiceBase<T> where T : class ,new()
    {
        bool Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> IncludeSubSets(params Expression<Func<T, object>>[] includeProperties);
    }
}