using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Enumerable
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> list, T item)
        {
            list.ToList().Add(item);
            return list;
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            
            foreach (T local in items)
            {
                action(local);
            }
        }


        public static System.Data.DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var dt = new System.Data.DataTable();
            var ps = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

            foreach (System.ComponentModel.PropertyDescriptor dp in ps)
                dt.Columns.Add(dp.Name, dp.PropertyType);
            if (data != null && data.Count() > 0)
            {
                foreach (T t in data)
                {
                    var dr = dt.NewRow();
                    foreach (System.ComponentModel.PropertyDescriptor dp in ps)
                        dr[dp.Name] = dp.GetValue(t);
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        /// <summary>
        ///   Convenient replacement for a range 'for' loop. e.g. return an array of int from 10 to 20:
        ///   int[] tenToTwenty = 10.to(20).ToArray();
        /// </summary>
        /// <param name = "from"></param>
        /// <param name = "to"></param>
        /// <returns></returns>
        public static IEnumerable<int> To(this int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                yield return i;
            }
        }

        /// <summary>
        ///   将ID数组转换成用逗号隔开的字符串
        /// </summary>
        /// <param name = "ids"></param>
        /// <returns></returns>
        public static string ToFormattedString(this int[] ids)
        {
            return ArrayToString(ids, false);
        }

        /// <summary>
        ///   将ID数组转换成用逗号隔开的字符串
        /// </summary>
        /// <param name = "ids"></param>
        /// <returns></returns>
        public static string IntArrayToString(this List<int> ids)
        {
            return ArrayToString(ids, false);
        }

        /// <summary>
        ///   将类型为T的列表转换为用逗号隔开的字符串
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "ids"></param>
        /// <param name = "withQuota"></param>
        /// <returns></returns>
        public static string ArrayToString<T>(this IList<T> ids, bool withQuota)
        {
            return ArrayToString(ids, ",", withQuota);
        }

        public static string ArrayToString<T>(IList<T> ids, string separator, bool withQuota)
        {
            if (ids == null)
                return null;

            var txt = new StringBuilder();
            if (withQuota)
            {
                foreach (T id in ids)
                {
                    txt.AppendFormat("'{0}'{1}", id, separator);
                }
            }
            else
            {
                foreach (T id in ids)
                {
                    txt.AppendFormat("{0}{1}", id, separator);
                }
            }
            return txt.Remove(txt.Length - 1, 1).ToString();
        }


    }
}
