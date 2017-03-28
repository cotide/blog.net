#region Using

using System.Collections;
using System.Web.Caching;

#endregion

namespace Cotide.Framework.Caching.Providers
{
    ///<summary>
    ///</summary>
    public class NullCache : ICache
    {
        #region ICache Members

        public object this[string key]
        {
            get { return null; }
        }

        public object Get(string itemKey)
        {
            return null;
        }

        public void ClearCache()
        {
        }

        public void Remove(string itemKey)
        {
        }

        public void Insert(string keyString, object value, int cacheDurationInSeconds, CacheItemPriority priority)
        {
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return new Hashtable().GetEnumerator();
        }

        public int GetCount()
        {
            return 0;
        }

        #endregion
    }
}