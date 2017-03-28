#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Caching;
using Cotide.Framework.Caching.Providers;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace Cotide.Framework.Caching.Manager
{
    ///<summary>
    ///</summary>
    public static class CacheHelper
    {
        #region Helper

        private static ICache GetCache()
        {
            return ServiceLocator.Current.GetInstance<ICache>();
        }

        private static IEnumerable<string> GetListOfCachedKeys()
        {
            IDictionaryEnumerator itemsInCache = GetCache().GetEnumerator();
            IList<string> keysInCache = new List<string>();
            while (itemsInCache.MoveNext())
            {
                keysInCache.Add(itemsInCache.Key.ToString());
            }
            return keysInCache;
        }

        #endregion

        /// <summary>
        /// Gets the get amount of items in cache.
        /// </summary>
        /// <value>The get amount of items in cache.</value>
        public static int GetAmountOfItemsInCache
        {
            get { return GetCache().GetCount(); }
        }

        /// <summary>
        /// Caches the object of T.
        /// If the object is in the cache then it will return that object
        /// or else it will insert <see cref="retrievalFunction"/> to the cache.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="retrievalFunction">This will run if the object is not in the cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>The object from the cache.</returns>
        public static T GetOrInsert<T>(string cacheKey, Func<T> retrievalFunction)
        {
            return GetOrInsert(cacheKey, CacheLength.GetDefaultCacheTime, retrievalFunction);
        }

        /// <summary>
        /// Caches the object of T.
        /// If the object is in the cache then it will return that object
        /// or else it will insert <see cref="retrievalFunction"/> to the cache.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="retrievalFunction">This will run if the object is not in the cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheDurationInSeconds">The cache duration in seconds.</param>
        /// <returns>The object from the cache.</returns>
        public static T GetOrInsert<T>(string cacheKey, int cacheDurationInSeconds, Func<T> retrievalFunction)
        {
            return GetOrInsert(cacheKey, cacheDurationInSeconds, CacheItemPriority.Normal, retrievalFunction);
        }

        /// <summary>
        /// Caches the object of T.
        /// If the object is in the cache then it will return that object
        /// or else it will insert <see cref="retrievalFunction"/> to the cache.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="retrievalFunction">This will run if the object is not in the cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheDurationInSeconds">The cache duration in seconds.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>The object from the cache.</returns>
        public static T GetOrInsert<T>(string cacheKey, int cacheDurationInSeconds, CacheItemPriority priority,
                                       Func<T> retrievalFunction)
        {
            object cachedObject = GetCache()[cacheKey];
            if (cachedObject == null)
            {
                cachedObject = retrievalFunction();
                if (cachedObject != null)
                    GetCache().Insert(cacheKey, cachedObject, cacheDurationInSeconds, priority);
            }
            return (T) cachedObject;
        }

        /// <summary>
        /// Removes the cache object.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public static void RemoveCacheObject(string cacheKey)
        {
            GetCache().Remove(cacheKey);
        }

        /// <summary>
        /// Removes all the objects from the cache that start with <see cref="keyStartsWith"/>.
        /// </summary>
        /// <param name="keyStartsWith">The starts with.</param>
        public static void RemoveAllCacheObjectStartWith(string keyStartsWith)
        {
            RemoveAll(item => item.StartsWith(keyStartsWith));
        }

        /// <summary>
        /// Removes all cache object that match the Regular Expression pattern.
        /// </summary>
        /// <param name="regxPattern">The regx.</param>
        public static void RemoveAllCacheObjectByPattern(string regxPattern)
        {
            Regex regex = new Regex(regxPattern);
            RemoveAll(item => regex.Match(item).Success);
        }

        /// <summary>
        /// Removes all Objects from the cache that match the condition.
        /// </summary>
        /// <param name="match">The Predicate<> delegate that defines the conditions of the elements to search for.</param>
        public static void RemoveAll(Predicate<string> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            foreach (string item in GetListOfCachedKeys())
            {
                if (match(item))
                    //this will remove the extra information from the end of the key.
                    GetCache().Remove(item);
            }
        }
    }
}