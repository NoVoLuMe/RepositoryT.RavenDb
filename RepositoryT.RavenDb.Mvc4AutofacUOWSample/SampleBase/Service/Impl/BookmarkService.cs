using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase.Repository;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase.Service.Impl
{
    public class BookmarkService : EntityService<Bookmark, IBookmarkRepository>, IBookmarkService
    {
        private const string CACHE_KEY = "Bookmark_Entitites";

        private List<Bookmark> BookmarkCache
        {
            get { return (List<Bookmark>)HttpContext.Current.Cache.Get(CACHE_KEY); }
        }

        public BookmarkService(IBookmarkRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            if (HttpContext.Current.Cache[CACHE_KEY] == null)
                CacheInit(Repository.GetAll().ToList());
        }

        public override void Add(Bookmark entity)
        {
            Repository.Add(entity);

            if (!string.IsNullOrEmpty(entity.Id))
            {
                AddToCache(entity);
            }

            UnitOfWork.Commit();
        }

        public override void Add(IEnumerable<Bookmark> entities)
        {
            Repository.Add(entities);
            UnitOfWork.Commit();
            AddToCache(entities);
        }

        public override void Update(Bookmark entity)
        {
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public override void Delete(Bookmark entity)
        {
            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

        public override void Delete(string id)
        {
            Repository.Delete(id);
            UnitOfWork.Commit();
            DeleteFromCache(id);
        }

        private void DeleteFromCache(string id)
        {
            List<Bookmark> bookmarkCache = BookmarkCache;
            Bookmark bookmarkToDelete = bookmarkCache.SingleOrDefault(item => item.Id.Equals(id));

            if (bookmarkToDelete != null)
            {
                bookmarkCache.Remove(bookmarkToDelete);
                CacheInit(bookmarkCache);
            }
        }

        public override void Delete(Expression<Func<Bookmark, bool>> @where)
        {
            Repository.Delete(@where);
            UnitOfWork.Commit();
        }

        public override IEnumerable<Bookmark> GetAll()
        {
            return BookmarkCache;
        }

        void AddToCache(Bookmark bookmark)
        {
            List<Bookmark> bookmarkCache = BookmarkCache;
            bookmarkCache.Add(bookmark);
            CacheInit(bookmarkCache);
        }

        private void AddToCache(IEnumerable<Bookmark> entities)
        {
            List<Bookmark> bookmarkCache = BookmarkCache;
            bookmarkCache.AddRange(entities);
            CacheInit(bookmarkCache);
        }

        private void CacheInit(List<Bookmark> value)
        {
            HttpContext.Current.Cache.Insert(CACHE_KEY, value, null,
                DateTime.Now.AddMinutes(10)
                , Cache.NoSlidingExpiration);
        }
    }
}