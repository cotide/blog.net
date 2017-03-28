#region Using

using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;
using Cotide.Framework.Caching.Providers;

#endregion

namespace Cotide.Framework.Caching.Manager
{
    /// <summary>
    /// The Cache Manager.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class CacheManager<TKey, TValue>
    {
        #region Member Variables

        private static readonly object _cacheLock = new object();
        private static CacheManager<TKey, TValue> _currentCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        private CacheManager()
        {
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value></value>
        public TValue this[TKey key]
        {
            get { return GetFromCache(key); }
        }

        #endregion

        #region Methods

        #region Public Static

        #region Cache Object Helpers

        /// <summary>
        /// Gets from cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static TValue GetFromCache(TKey key)
        {
            return (TValue) GetFromCache(MakeKey(key));
        }

        /// <summary>
        /// Gets from cache object.
        /// This doesn't use any boxing.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static object GetFromCacheObject(TKey key)
        {
            return GetFromCache(MakeKey(key));
        }

        /// <summary>
        /// Gets from cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static object GetFromCache(string key)
        {
            return GenericCacheManager.GetInstance().Cache[key];
        }

        #endregion

        /// <summary>
        /// Gets the instance of the cache manager.
        /// </summary>
        /// <returns></returns>
        public static CacheManager<TKey, TValue> GetInstance()
        {
            if (_currentCache == null)
            {
                lock (_cacheLock)
                {
                    if (_currentCache == null)
                    {
                        _currentCache = new CacheManager<TKey, TValue>();
                    }
                }
            }
            return _currentCache;
        }

        /// <summary>
        /// Reloads the cache provider and its settings.
        /// </summary>
        public void ReloadCacheProvider()
        {
            GenericCacheManager.GetInstance().ReloadCacheProvider();
        }

        #endregion

        #region Public

        /// <summary>
        /// Determines whether the Cache contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the cache contains the key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return GetFromCacheObject(key) != null;
        }

        /// <summary>
        /// Gets the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue Get(TKey key)
        {
            return GetFromCache(key);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Insert(TKey key, TValue value)
        {
            Insert(key, value, CacheLength.GetDefaultCacheTime);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDurationInSeconds">The cache duration in seconds.</param>
        public void Insert(TKey key, TValue value, int cacheDurationInSeconds)
        {
            Insert(key, value, cacheDurationInSeconds, CacheItemPriority.Default);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDurationInSeconds">The cache duration in seconds.</param>
        /// <param name="priority">The priority.</param>
        public void Insert(TKey key, TValue value, int cacheDurationInSeconds, CacheItemPriority priority)
        {
            Cache.Insert(MakeKey(key), value, cacheDurationInSeconds, priority);
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(TKey key)
        {
            Cache.Remove(MakeKey(key));
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            Cache.ClearCache();
        }

        /// <summary>
        /// Gets the amount of items in the cache.
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return Cache.GetCount();
        }

        #endregion

        #region Private

        /// <summary>
        /// Creates the key used for identifying the cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string MakeKey(TKey key)
        {
            return key + "|" + typeof (TKey) + key.GetHashCode();
        }

        #endregion

        #region internal

        /// <summary>
        /// Gets a list of cached objects.
        /// </summary>
        /// <returns></returns>
        internal IList<string> GetListOfCachedKeys()
        {
            IDictionaryEnumerator itemsInCache = Cache.GetEnumerator();
            IList<string> keysInCache = new List<string>();
            while (itemsInCache.MoveNext())
            {
                keysInCache.Add(itemsInCache.Key.ToString());
            }
            return keysInCache;
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        internal void RemoveByKey(string key)
        {
            Cache.Remove(key);
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the cache provider.
        /// </summary>
        /// <value>The cache provider.</value>
        public ICache Cache
        {
            get { return GenericCacheManager.GetInstance().Cache; }
        }

        #endregion
    }

    /// <summary>
    /// This is a Generic Cache Manager That doesn't know the type of object that you are using
    /// That way it just loads once the settings that don't have anything with the type.
    /// </summary>
    internal class GenericCacheManager
    {
        #region Member Variables

        private const bool DEFAULT_USE_CACHE = false;
        private static readonly object _thisLock = new object();
        private static GenericCacheManager _genericCacheManager;

        private ICache _cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCacheManager"/> class.
        /// </summary>
        private GenericCacheManager()
        {
            ReloadGenericCacheManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a instance of this class.
        /// </summary>
        /// <returns></returns>
        public static GenericCacheManager GetInstance()
        {
            if (_genericCacheManager == null)
            {
                lock (_thisLock)
                {
                    if (_genericCacheManager == null)
                    {
                        _genericCacheManager = new GenericCacheManager();
                    }
                }
            }
            return _genericCacheManager;
        }

        /// <summary>
        /// Reloads the generic cache manager.
        /// </summary>
        private void ReloadGenericCacheManager()
        {
            bool _useCache = DEFAULT_USE_CACHE;
            _cache = SetCacheSettings(_useCache);
        }

        /// <summary>
        /// Sets the cache settings.
        /// </summary>
        /// <param name="useCache">if set to <c>true</c> [use cache].</param>
        private static ICache SetCacheSettings(bool useCache)
        {
            return useCache ? (ICache) new HttpRuntimeCache() : new NullCache();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cache provider.
        /// </summary>
        /// <value>The cache provider.</value>
        public ICache Cache
        {
            get { return _cache; }
            private set { _cache = value; }
        }

        /// <summary>
        /// Reloads the cache provider and its settings.
        /// </summary>
        public void ReloadCacheProvider()
        {
            ReloadGenericCacheManager();
        }

        #endregion
    }
}