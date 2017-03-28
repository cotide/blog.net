//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：BaseEntityDesc.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012-12-19 10:17:18 
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
    /// 抽象 - 实体描述
    /// </summary>
    public abstract class BaseEntityDescAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public readonly string Description;
          
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">描述</param> 
        protected BaseEntityDescAttribute(string description)
        { 
            Description = description;
        } 
    }
}
