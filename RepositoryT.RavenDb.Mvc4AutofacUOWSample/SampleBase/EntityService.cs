using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase
{
    public class EntityService<TEntity, TRepository>
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public EntityService(TRepository repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public virtual void Add(TEntity entity)
        {
            Repository.Add(entity);
            UnitOfWork.Commit();
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Repository.Add(entities);
            UnitOfWork.Commit();
        }

        public virtual void Update(TEntity entity)
        {
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

        public virtual void Delete(string id)
        {
            Repository.Delete(id);
            UnitOfWork.Commit();
        }


        public virtual void Delete(Expression<Func<TEntity, bool>> @where)
        {
            Repository.Delete(@where);
            UnitOfWork.Commit();
        }

        public virtual TEntity GetById(long id)
        {
            return Repository.GetById(id);
        }

        public virtual TEntity GetById(string id)
        {
            return Repository.GetById(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> @where)
        {
            return Repository.GetMany(@where);
        }

        public virtual IQueryable<TEntity> IncludeSubSets(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Repository.IncludeSubSets(includeProperties);
        }
    }
}