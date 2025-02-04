using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace KPBrokers.Submission.Quote.UI.Services.Caching
{
    public class RuntimeCacheService : CacheServiceBase
    {
        private readonly ObjectCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeCacheService"/> class.
        /// </summary>
        public RuntimeCacheService()
            : this(MemoryCache.Default, new TimeSpan(0, 20, 0))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeCacheService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="defaultSlidingDuration">Default duration of the sliding.</param>
        public RuntimeCacheService(ObjectCache cache, TimeSpan defaultSlidingDuration)
            : base(defaultSlidingDuration)
        {
            //Point the ObjectCache to a derived class which implements the appropriate policy
            _cache = cache;
        }

        #region Implementation of ICacheService

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool Exists(string key)
        {
            return _cache.Contains(key);
        }

        /// <summary>
        /// Private method which contains business logic for saving / creating items in cache
        /// </summary>
        /// <param name="key">The key of the item to be saved</param>
        /// <param name="value">The value of the item to be saved</param>
        /// <param name="policy">The expiration policy of the item to be saved</param>
        protected override bool Save(string key, object value, CacheItemPolicy policy)
        {
            if (Exists(key))
            {
                _cache.Set(key, value, policy);
                return true;
            }
            else
            {
                return _cache.Add(key, value, policy);
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Get(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        /// Gets the specified keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public override IDictionary<string, object> Get(string[] keys)
        {
            return _cache.GetValues(keys);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool Remove(string key)
        {
            //Remove the item from the cache if it exists...
            if (Exists(key))
            {
                _cache.Remove(key);
                return !Exists(key); //Return true or false depending upon if the item still exists..
            }

            return false; //Return false if the item never existed
        }

        /// <summary>
        /// Gets all cache.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<KeyValuePair<string, object>> GetAllCache()
        {
           var caches = _cache.Where(x=>x.Key.StartsWith("KPBROKERSCACHED")).ToList();
            return caches;
        }

        #endregion
    }
}