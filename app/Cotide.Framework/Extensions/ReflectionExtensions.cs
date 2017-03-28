#region using

using System;
using System.Reflection;

#endregion

namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// 反射扩展方法类
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// 获取某个属性
        /// </summary>
        /// <typeparam name="TAttribute">属性类型</typeparam>
        /// <param name="member">属性</param>
        /// <returns>返回属性(不存在即返回null)</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            var attributes = member.GetCustomAttributes(typeof (TAttribute), true);
            if (attributes != null && attributes.Length > 0)
                return (TAttribute) attributes[0];
            return null;
        }

        /// <summary>
        /// 判断是否存在某个属性
        /// </summary>
        /// <typeparam name="TAttribute">属性类型</typeparam>
        /// <param name="member">属性</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            return member.GetAttribute<TAttribute>() != null;
        }
    }
}