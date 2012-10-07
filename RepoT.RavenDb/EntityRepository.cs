using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Linq;
using RepoT.Infrastructure;

namespace RepoT.RavenDb
{
    public abstract class EntityRepository<T> : RepositoryBase<IDocumentSession> where T : class
    {
        protected EntityRepository(IDatabaseFactory<IDocumentSession> databaseFactory) :
            base(databaseFactory)
        {

        }

        public virtual void Add(T entity)
        {
            DataContext.Store(entity);
        }

        /// <summary>
        /// Abtract method for update operations
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(T entity);

        public virtual void Delete(T entity)
        {
            DataContext.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> @where)
        {
            IRavenQueryable<T> result = DataContext.Query<T>().Where(@where);
            result.ForEach(item => DataContext.Delete(item));
        }

        public virtual T GetById(long id)
        {
            return DataContext.Load<T>(id);
        }

        public virtual T GetById(string id)
        {
            return DataContext.Load<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Enumerable.ToList(DataContext.Load<T>());
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return Enumerable.ToList(DataContext.Query<T>().Where(where));
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return Queryable.FirstOrDefault<T>(DataContext.Query<T>().Where(where));
        }

        public virtual IQueryable<T> IncludeSubSets(params Expression<Func<T, object>>[] includeProperties)
        {
            IRavenQueryable<T> ravenQueryable = DataContext.Query<T>();
            foreach (var includeProperty in includeProperties)
            {
                ravenQueryable = ravenQueryable.Include(includeProperty);
            }
            return ravenQueryable;
        }
    }
}