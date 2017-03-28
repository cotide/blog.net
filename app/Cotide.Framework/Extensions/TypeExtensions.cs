using System;

namespace Cotide.Framework.Extensions
{
    public static class TypeExtensions
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            
            System.Reflection.MemberInfo info = type;
            return Attribute.GetCustomAttribute(info, typeof(T)) as T;
        }
         
    }
}