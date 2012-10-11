using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
using RepoT.Infrastructure;
using RepoT.RavenDb.MvcSample.Models;

namespace RepoT.RavenDb.MvcSample.SampleBase
{
    public class BookmarkService : IServiceBase<Bookmark>
    {
        private const string CACHE_KEY = "Bookmark_Entitites";
        private readonly BookmarkRepository _repository;

        private List<Bookmark> BookmarkCache
        {
            get { return (List<Bookmark>)HttpContext.Current.Cache.Get(CACHE_KEY); }
        }

        public BookmarkService(BookmarkRepository repository)
        {
            _repository = repository;
            if (HttpContext.Current.Cache[CACHE_KEY] == null)
                CacheInit(_repository.GetAll().ToList());
        }

        private void CacheInit(List<Bookmark> value)
        {
            HttpContext.Current.Cache.Insert(CACHE_KEY, value, null,
                DateTime.Now.AddMinutes(10)
                , Cache.NoSlidingExpiration);
        }

        public bool Add(Bookmark entity)
        {
            bool result = _repository.Add(entity);

            if (result)
            {
                AddToCache(entity);
            }
            return result;
        }

        public void Update(Bookmark entity)
        {
            _repository.Update(entity);
        }

        public void Delete(Bookmark entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public void Delete(Expression<Func<Bookmark, bool>> @where)
        {
            _repository.Delete(@where);
        }

        public Bookmark GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Bookmark GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Bookmark Get(Expression<Func<Bookmark, bool>> @where)
        {
            return _repository.Get(@where);
        }

        public IEnumerable<Bookmark> GetAll()
        {
            return BookmarkCache;
        }

        public IEnumerable<Bookmark> GetMany(Expression<Func<Bookmark, bool>> @where)
        {
            return _repository.GetMany(@where);
        }

        public IQueryable<Bookmark> IncludeSubSets(params Expression<Func<Bookmark, object>>[] includeProperties)
        {
            return _repository.IncludeSubSets(includeProperties);
        }

        void AddToCache(Bookmark bookmark)
        {
            List<Bookmark> bookmarkCache = BookmarkCache;
            bookmarkCache.Add(bookmark);
            CacheInit(bookmarkCache);
        }
    }
}