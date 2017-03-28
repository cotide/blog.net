#region Using

using System;
using System.Collections.Generic;

#endregion

namespace Cotide.Framework.Extensions
{
    ///<summary>
    ///</summary>
    public static class EnumerableExtension
    {
        ///<summary>
        ///</summary>
        ///<param name="enumerable"></param>
        ///<param name="action"></param>
        ///<typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}