#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection; 

#endregion

namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获得某个Enum类型的绑定列表<br/>
        /// </summary>
        /// <param name="en">枚举的类型，例如：typeof(Sex)</param>
        /// <returns>
        /// 键值集合 key为Man,value为"男"(特性的描述值,如果没有特性描述就显示英文的字段名)       
        /// </returns>
        public static Dictionary<string, int> ToDictionary(this Enum en)
        {
            var enumType = en.GetType();
            //建立DataTable的列信息
            var dic = new Dictionary<string, int>();

            //获得特性Description的类型信息
            Type typeDescription = typeof(DescriptionAttribute);

            //获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            System.Reflection.FieldInfo[] fields = enumType.GetFields();

            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                //过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {
                    int value=-1;
                    string text = string.Empty;
                    // 通过字段的名字得到枚举的值
                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内
                    value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                    //获得这个字段的所有自定义特性，这里只查找Description特性
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        //因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        //获得特性的描述值，也就是‘男’‘女’等中文描述
                        text = aa.Description;
                    }
                    else
                    {
                        //如果没有特性描述（-_-!忘记写了吧~）那么就显示英文的字段名
                        text = field.Name;
                    }
                    dic.Add(text, value);
                }
            }

            return dic;
        }

        /// <summary>
        /// 获取枚举标记中说明文字<br/>
        /// (枚举成员的[Description("说明文字")],使用说明:MyEnum.Test.GetDescription())
        /// </summary>
        /// <param name="en">枚举成员</param>
        /// <returns>返回说明文字,如果没有则返回英文的字段名</returns>
        public static string GetDescription(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute) attrs[0]).Description;
            }
            return en.ToString();
        }

        /// <summary>
        ///  枚举转换为列表(元素为枚举成员标记中的说明文字)
        /// </summary>
        /// <param name = "type">枚举类型</param>
        /// <returns>返回列表</returns>
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        ///  枚举转换为列表(元素为枚举成员标记中的说明文字)
        /// </summary>
        /// <param name = "type">枚举类型</param>
        /// <returns>返回列表</returns>
        public static IList ToList<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<T, string>((T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        ///  枚举转换为列表(元素为键值对,键为枚举成员,值枚举成员标记中的说明文字)
        /// </summary>
        /// <param name = "type">枚举类型</param>
        /// <returns>返回列表</returns>
        public static IList ToExtendedList<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValueTriplet<Enum, T, string>(value, (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        /// 位枚举,得到对应的枚举值
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static IList<int> FlagsToList(this Enum en)
        {
            var list = new List<int>();
            if (en != null)
            {
                var type = en.GetType();
                var strList = en.ToString().Split(',');
                foreach (var s in strList)
                {
                    var enm = Enum.Parse(type, s.Trim());
                    if (enm != null)
                    {
                        list.Add(enm.GetHashCode());
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 位枚举,得到对应的枚举标记中的说明文字
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static List<string> GetDescriptionList(this Enum en)
        {
            var descriptionList = new List<string>();
            if (en == null)
                return descriptionList;
            //加上支持位移
            var type = en.GetType();
            var strList = en.ToString(CultureInfo.InvariantCulture).Split(',');
            foreach (var s in strList)
            {
                var memInfo = type.GetMember(s.Trim());
                if (memInfo.Length <= 0) continue;
                memInfo[0].GetAttribute<DescriptionAttribute>();
                var description = memInfo[0].GetCustomAttributesData().First().ConstructorArguments.First().Value.ToString();
                descriptionList.Add(description);
            }
            return descriptionList;
        }
         
    } 
}