using System;
using System.Collections.Generic;
using System.Globalization;
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
        protected EntityRepository(IDataContextFactory<IDocumentSession> dataContextFactory) :
            base(dataContextFactory)
        {

        }

        public virtual bool Add(T entity)
        {
            bool result;

            try
            {
                DataContext.Store(entity);
                DataContext.SaveChanges();
                //DataContext.Dispose();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public virtual void Update(T entity)
        {
            DataContext.Store(entity);
            DataContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            DataContext.Delete(entity);
            DataContext.SaveChanges();
        }
        public virtual void Delete(int id)
        {
            Delete(id.ToString(CultureInfo.InvariantCulture));
            DataContext.SaveChanges();
        }

        public virtual void Delete(string id)
        {
            DataContext.Advanced.DatabaseCommands.Delete(id.ToString(CultureInfo.InvariantCulture), null);
            DataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> @where)
        {
            IRavenQueryable<T> result = DataContext.Query<T>().Where(@where);
            result.ForEach(item => DataContext.Delete(item));
            DataContext.SaveChanges();
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
            return Enumerable.ToList(DataContext.Query<T>());
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