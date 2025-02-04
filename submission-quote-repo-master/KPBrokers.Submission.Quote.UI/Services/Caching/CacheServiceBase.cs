using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace KPBrokers.Submission.Quote.UI.Services.Caching
{
    /// <summary>
    /// Abstract base class for working with cache service implementations
    /// </summary>
    public abstract class CacheServiceBase : ICacheService
    {

        protected readonly TimeSpan _default_duration;

        protected CacheServiceBase(TimeSpan defaultDuration)
        {
            _default_duration = defaultDuration;
        }

        #region abstract ICacheService members

        /// <summary>
        /// Private method which contains business logic for saving / creating items in cache
        /// </summary>
        /// <param name="key">The key of the item to be saved</param>
        /// <param name="value">The value of the item to be saved</param>
        /// <param name="policy">The expiration policy of the item to be saved</param>
        protected abstract bool Save(string key, object value, CacheItemPolicy policy);

        /// <summary>
        /// Retrieves a cached object from the cache
        /// </summary>
        /// <param name="key">The key used to identify this object</param>
        /// <returns>
        /// The object from the database, or an exception if the object doesn't exist
        /// </returns>
        public abstract object Get(string key);

        /// <summary>
        /// Gets a set of cached objects
        /// </summary>
        /// <param name="keys">All of the keys of the objects we wish to retrieve</param>
        /// <returns>
        /// A key / value dictionary containing all of the keys and objects we wanted to retrieve
        /// </returns>
        public abstract IDictionary<string, object> Get(string[] keys);

        /// <summary>
        /// Removes an object from the cache with the specified key
        /// </summary>
        /// <param name="key">The key used to identify this object</param>
        /// <returns>
        /// True if the object was removed, false if it didn't exist or was unable to be removed
        /// </returns>
        public abstract bool Remove(string key);

        #endregion

        #region Implementation of ICacheService

        /// <summary>
        /// Validates if an object with this key exists in the cache
        /// </summary>
        /// <param name="key">The key of the object to check</param>
        /// <returns>
        /// True if it exists, false if it doesn't
        /// </returns>
        public abstract bool Exists(string key);


        public abstract IEnumerable<KeyValuePair<string, object>> GetAllCache();        

        /// <summary>
        /// Insert an item into the cache with no specifics as to how it will be used
        /// </summary>
        /// <param name="key">The key used to map this object</param>
        /// <param name="value">The value to be saved to the cache</param>
        /// <returns></returns>
        public bool Save(string key, object value)
        {
            return Save(key, value, GetDefaultPolicy());
        }

        /// <summary>
        /// Insert an item in the cache with the expiration that it will expire if not used past its window
        /// </summary>
        /// <param name="key">The key used to map this object</param>
        /// <param name="value">The object to insert</param>
        /// <param name="slidingExpiration">The expiration window</param>
        /// <returns></returns>
        public bool Save(string key, object value, TimeSpan slidingExpiration)
        {
            return Save(key, value, GetSlidingPolicy(slidingExpiration));
        }

        /// <summary>
        /// Insert an item in the cache with the expiration that will expire at a specific point in time
        /// </summary>
        /// <param name="key">The key used to map this object</param>
        /// <param name="value">The object to insert</param>
        /// <param name="absoluteExpiration">The DateTime in which this object will expire</param>
        /// <returns></returns>
        public bool Save(string key, object value, DateTime absoluteExpiration)
        {
            return Save(key, value, GetAbsolutePolicy(absoluteExpiration));
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Get(key); }
            set { Save(key, value, GetDefaultPolicy()); }
        }

        #endregion


        #region Expiration Policy Helpers

        /// <summary>
        /// Gets the default policy.
        /// </summary>
        /// <returns></returns>
        protected CacheItemPolicy GetDefaultPolicy()
        {
            return new CacheItemPolicy() { SlidingExpiration = _default_duration };
        }

        /// <summary>
        /// Gets the absolute policy.
        /// </summary>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        /// <returns></returns>
        protected CacheItemPolicy GetAbsolutePolicy(DateTime absoluteExpiration)
        {
            return new CacheItemPolicy { AbsoluteExpiration = absoluteExpiration };
        }

        /// <summary>
        /// Gets the sliding policy.
        /// </summary>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        /// <returns></returns>
        protected CacheItemPolicy GetSlidingPolicy(TimeSpan slidingExpiration)
        {
            return new CacheItemPolicy { SlidingExpiration = slidingExpiration };
        }
        #endregion
    }
}