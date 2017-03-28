namespace Cotide.Framework.Caching.Manager
{
    ///<summary>
    ///</summary>
    public static class CacheLength
    {
        #region Constants

        private const int SHORT_CACHE_TIME = 120; // 2 minutes
        private const int NORMAL_CACHE_TIME = 300; // 5 minutes
        private const int LONG_CACHE_TIME = 1500; // 25 minutes

        ///<summary>
        ///</summary>
        public const string DEFAULT_CACHE = NORMAL_CACHE;

        ///<summary>
        /// 短时间缓存
        ///</summary>
        public const string SHORT_CACHE = "SHORT_CACHE_TIME";

        ///<summary>
        /// 普通缓存
        ///</summary>
        public const string NORMAL_CACHE = "NORMAL_CACHE_TIME";

        ///<summary>
        /// 长时间缓存
        ///</summary>
        public const string LONG_CACHE = "LONG_CACHE_TIME";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the get default cache time.
        /// </summary>
        /// <value>The get default cache time.</value>
        public static int GetDefaultCacheTime
        {
            get { return GetCacheLengthByKey(DEFAULT_CACHE); }
        }

        /// <summary>
        /// Gets the get default cache time.
        /// </summary>
        /// <value>The get default cache time.</value>
        public static int GetNormalCacheTime
        {
            get { return GetCacheLengthByKey(NORMAL_CACHE); }
        }

        /// <summary>
        /// Gets the get ten minutes cache time.
        /// </summary>
        /// <value>The get ten minutes cache time.</value>
        public static int GetShortCacheTime
        {
            get { return GetCacheLengthByKey(SHORT_CACHE); }
        }

        /// <summary>
        /// Gets the get long cache time.
        /// </summary>
        /// <value>The get long cache time.</value>
        public static int GetLongCacheTime
        {
            get { return GetCacheLengthByKey(LONG_CACHE); }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Gets the cache length by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static int GetCacheLengthByKey(string key)
        {
            switch (key)
            {
                case SHORT_CACHE:
                    return SHORT_CACHE_TIME;
                case NORMAL_CACHE:
                    return NORMAL_CACHE_TIME;
                case LONG_CACHE:
                    return LONG_CACHE_TIME;
                default:
                    return NORMAL_CACHE_TIME;
            }
        }

        #endregion
    }
}