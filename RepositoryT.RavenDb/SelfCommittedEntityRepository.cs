using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Linq;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb
{
    public abstract class SelfCommittedEntityRepository<T> : EntityRepository<T> where T : class
    {
        protected SelfCommittedEntityRepository(IDataContextFactory<IDocumentSession> dataContextFactory) :
            base(dataContextFactory)
        {

        }

        public override void Add(T entity)
        {
            DataContext.Store(entity);
            SaveChanges();
        }

        public override void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DataContext.Store(entity);
            }

            SaveChanges();
        }

        public override void Update(T entity)
        {
            DataContext.Store(entity);
            SaveChanges();
        }

        public override void Delete(T entity)
        {
            DataContext.Delete(entity);
            SaveChanges();
        }
        public override void Delete(int id)
        {
            Delete(id.ToString(CultureInfo.InvariantCulture));
            SaveChanges();
        }

        public override void Delete(string id)
        {
            DataContext.Advanced.DatabaseCommands.Delete(id.ToString(CultureInfo.InvariantCulture), null);
            SaveChanges();
        }

        public override void Delete(Expression<Func<T, bool>> @where)
        {
            IRavenQueryable<T> result = DataContext.Query<T>().Where(@where);
            result.ForEach(item => DataContext.Delete<T>(item));

            SaveChanges();
        }

        private void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}