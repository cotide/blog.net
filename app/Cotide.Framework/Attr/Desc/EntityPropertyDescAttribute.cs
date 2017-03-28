//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：EntityPropertyAttribute.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012-12-19 10:23:40 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;

namespace Cotide.Framework.Attr.Desc
{
    /// <summary>
    /// 实体属性描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class EntityPropertyDescAttribute : BaseEntityDescAttribute
    {
        /// <summary> 
        /// 构造函数
        /// </summary>
        /// <param name="description">实体描述</param>
        public EntityPropertyDescAttribute(string description)
            : base(description)
        {
        }
    }
}
