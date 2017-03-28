using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// String扩展
    /// </summary>
    public  static class StringExtensions
    {
        /// <summary>
        ///     Switches the IsNullOrEmpty to a more readable format
        /// </summary>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        /// <returns>
        ///     True if the string is null or empty
        /// </returns>
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        /// <summary>
        ///     Switches the IsNotNullOrEmpty to a more readable format
        /// </summary>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        /// <returns>
        ///     True if the string is not null or empty
        /// </returns>
        public static bool IsNotNullOrEmpty(this string theString)
        {
            return !string.IsNullOrEmpty(theString);
        }

        /// <summary>
        ///     Formats a string with the specified args
        /// </summary>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        /// <param name = "args">
        ///     The format args.
        /// </param>
        /// <returns>
        ///     The formatted string
        /// </returns>
        public static string FormatWith(this string theString, params object[] args)
        {
            return string.Format(theString, args);
        }

        /// <summary>
        ///     Retuns the frist N Characters of a string
        /// </summary>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        /// <param name = "index">
        ///     The index.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref = "InvalidOperationException">
        /// </exception>
        public static string FirstNCharacters(this string theString, int index)
        {
            if (theString == null)
                return string.Empty;

            int length = theString.Trim().Length;

            if (index >= length)
            {
                index = length;
            }

            return theString.Substring(0, index);
        }

        /// <summary>
        ///     Returns the last N Characters of a string.
        /// </summary>
        /// <remarks>
        ///     "Hello World ".LastNCharacters(5) returns "World"
        /// </remarks>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        /// <param name = "index">
        ///     The index.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref = "InvalidOperationException">
        /// </exception>
        public static string LastNCharacters(this string theString, int index)
        {
            if (theString == null)
                return string.Empty;

            int length = theString.Trim().Length;

            if (index > length)
            {
                throw new InvalidOperationException("string must be longer than the index value");
            }

            return theString.Substring((length - index), index);
        }

        /// <summary>
        ///     Appends an Html Line break and the string
        ///     to a string builder if the string is
        ///     not null or empty
        /// </summary>
        /// <param name = "sb">
        ///     The string builder.
        /// </param>
        /// <param name = "theString">
        ///     The the string.
        /// </param>
        public static void AppendHtmlLineIfNotEmpty(this StringBuilder sb, string theString)
        {
            if (!theString.IsNullOrEmpty())
            {
                sb.AppendFormat("<BR />{0}", theString);
            }
        }

        /// <summary>
        ///     Firsts the N words.
        /// </summary>
        /// <param name = "theString">The string.</param>
        /// <param name = "numWords">The num words.</param>
        /// <returns>
        ///     The first N words from a string, if shorter then the original string is returned.
        /// </returns>
        public static string FirstNWords(this string theString, int numWords)
        {
            StringBuilder sb = new StringBuilder();

            if (theString != null && numWords >= 0)
            {
                string[] words = theString.Split(' ');

                IEnumerator enumerator = words.GetEnumerator();

                int count = 0;
                while (count < numWords)
                {
                    if (count != 0)
                    {
                        sb.Append(" ");
                    }

                    if (enumerator.MoveNext())
                    {
                        sb.Append(enumerator.Current);
                        enumerator.MoveNext();
                        count++;
                    }
                    else
                    {
                        // die quietly
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///转换为DateTime
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string theString)
        {
            if (!string.IsNullOrEmpty(theString))
            {
                DateTime result;
                if (DateTime.TryParse(theString, out result))
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        ///转换为int
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static int? ToInt(this string theString)
        {
            if (!string.IsNullOrEmpty(theString))
            {
                int result;
                if (int.TryParse(theString, out result))
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加Url参数到Url字符串后面
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string AddUrlParas(this string theString, IDictionary<string, string> paras)
        {
            var norparastr = string.Join("&",
                                         paras.Select(m => string.Format("{0}={1}", m.Key, m.Value)).ToArray());
            if (theString.Contains("?"))
            {
                return string.Format("{0}&{1}", theString, norparastr);
            }
            return string.Format("{0}?{1}", theString, norparastr);
        }

        /// <summary>
        /// MD5加密码字符串
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string ToMD5(this string theString)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            string encoded = BitConverter.ToString(md5.ComputeHash(new UTF8Encoding(false).GetBytes(theString))).Replace("-", "");
            return encoded;
        }

         
    }
}
