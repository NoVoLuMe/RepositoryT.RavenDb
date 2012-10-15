using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase
{
    public class CacheManager
    {
        public TimeSpan CacheDuration { get; set; }
        public List<string> Keys { get; set; }
        private static readonly object SyncObject = new object();

        public CacheManager(TimeSpan duration)
        {

            Keys = new List<string>();
            CacheDuration = duration;

        }

        public T GetAndCheck<T>(string key, Func<T> getItem)
        {
            if (HasKey(key))
            {
                return Grab<T>(key);
            }

            lock (SyncObject)
            {
                if (HasKey(key))
                {
                    return Grab<T>(key);
                }
                T item = getItem(); Insert<T>(key, item, CacheItemPriority.Default); return item;
            }
        }

        private T Grab<T>(string key) { return (T)HttpContext.Current.Cache[key]; }

        private bool HasKey(string key)
        {
            return HttpContext.Current.Cache[key] != null && Keys.Contains(key);
        }

        private void Insert<T>(string key, T obj, CacheItemPriority priority)
        {
            if (!Keys.Contains(key)) Keys.Add(key);
            DateTime expiration = DateTime.Now.Add(CacheDuration);
            HttpContext.Current.Cache.Add(key, obj, null, expiration, TimeSpan.Zero, priority, null);
        }
        public void ClearAll()
        {
            lock (SyncObject)
            {
                foreach (string key in Keys)
                {
                    HttpContext.Current.Cache.Remove(key);
                }

                Keys.Clear();
            }
        }
        public void Clear(string key)
        {
            lock (SyncObject)
            {
                if (Keys.Contains(key)) Keys.Remove(key);
                HttpContext.Current.Cache.Remove(key);
            }
        }
    }
}