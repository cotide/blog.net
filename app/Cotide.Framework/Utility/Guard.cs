using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Framework.Extensions;
using Cotide.Framework.Resources;

namespace Cotide.Framework.Utility
{
     /// <summary>
    ///   Helper class for argument validation.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///   Ensures the specified argument is not null.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, TextResource.CannotBeNull.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified string is not blank.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty((parameter ?? string.Empty)))
            {
                throw new ArgumentException(TextResource.CannotBeNullOrEmpty.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified array is not null or empty.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty<T>(T[] parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);

            if (parameter.Length == 0)
            {
                throw new ArgumentException(TextResource.ArrayCannotBeEmpty.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified collection is not null or empty.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty<T>(ICollection<T> parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);

            if (parameter.Count == 0)
            {
                throw new ArgumentException(TextResource.CollectionCannotBeEmpty.FormatWith(parameterName),
                                            parameterName);
            }
        }

        /// <summary>
        ///   Ensures the specified value is a positive integer.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotZeroOrNegative(int parameter, string parameterName)
        {
            if (parameter <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                                                      TextResource.CannotBeNegativeOrZero.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified value is not a negative integer.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNegative(int parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                                                      TextResource.CannotBeNegative.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified value is not a negative float.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotNegative(float parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                                                      TextResource.CannotBeNegative.FormatWith(parameterName));
            }
        }

        /// <summary>
        ///   Ensures the specified path is a virtual path which starts with ~/.
        /// </summary>
        /// <param name = "parameter">The parameter.</param>
        /// <param name = "parameterName">Name of the parameter.</param>
        public static void IsNotVirtualPath(string parameter, string parameterName)
        {
            IsNotNullOrEmpty(parameter, parameterName);

            if (!parameter.StartsWith("~/", StringComparison.Ordinal))
            {
                throw new ArgumentException(TextResource.SourceMustBeAVirtualPathWhichShouldStartsWithTileAndSlash, parameterName);
            }
        }

        public static void IsNotOutOfLength(string parameter, int length, string parameterName)
        {
            if (parameter.Trim().Length > length)
            {
                throw new ArgumentException("\"{0}\" cannot be more than {1} character.".FormatWith(parameterName, length), parameterName);
            }
        }

        public static void IsNotBelowOfLength(string parameter, int length, string parameterName)
        {
            if (parameter.Trim().Length < length)
            {
                throw new ArgumentException("\"{0}\" must be more than {1} character.".FormatWith(parameterName, length), parameterName);
            }
        }

        public static void EqualsNotType(object parameter, object type, string parameterName)
        {
            if (!parameter.Equals(type))
            {
                throw new ArgumentException(TextResource.EqualsType.FormatWith(parameterName, type), parameterName);
            }
        }

        public static void EqualsType(object parameter, object type, string parameterName)
        {
            if (parameter.Equals(type))
            {
                throw new ArgumentException(TextResource.EqualsType.FormatWith(parameterName, type), parameterName);
            }
        }
    }
}
