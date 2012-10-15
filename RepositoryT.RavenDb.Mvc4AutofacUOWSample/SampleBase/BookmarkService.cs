using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase
{
    public class BookmarkService : IBookmarkService
    {
        private const string CACHE_KEY = "Bookmark_Entitites";
        private readonly IBookmarkRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private List<Bookmark> BookmarkCache
        {
            get { return (List<Bookmark>)HttpContext.Current.Cache.Get(CACHE_KEY); }
        }

        public BookmarkService(IBookmarkRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (HttpContext.Current.Cache[CACHE_KEY] == null)
                CacheInit(Enumerable.ToList<Bookmark>(_repository.GetAll()));
        }

       public void Add(Bookmark entity)
        {
            _repository.Add(entity);

            if (!string.IsNullOrEmpty(entity.Id))
            {
                AddToCache(entity);
            }

            _unitOfWork.Commit();
        }

        public void Add(IEnumerable<Bookmark> entities)
        {
            _repository.Add(entities);
            _unitOfWork.Commit();
            AddToCache(entities);
        }

        public void Update(Bookmark entity)
        {
            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Bookmark entity)
        {
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
            _unitOfWork.Commit();
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

        public void Delete(Expression<Func<Bookmark, bool>> @where)
        {
            _repository.Delete(@where);
            _unitOfWork.Commit();
        }

        public Bookmark GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Bookmark GetById(string id)
        {
            return _repository.GetById(id);
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