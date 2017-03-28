//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：EntityDescAttribute.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012-12-19 10:18:42 
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
    /// 实体描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class EntityDescAttribute : BaseEntityDescAttribute
    { 
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">描述</param>
         public EntityDescAttribute(string description)
             : base(description){ 
        }
    }
}
