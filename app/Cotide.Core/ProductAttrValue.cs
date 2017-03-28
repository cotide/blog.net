//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductAttrValue.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 16:42:17 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 商品业务属性值
    /// </summary>
    [EntityDesc("商品业务属性值")]
    public class ProductAttrValue : Entity
    {
        /// <summary>
        /// 属性值标记
        /// </summary>
        [EntityPropertyDesc("属性值标记")]
        public virtual string Key { get; set; }

        /// <summary>
        /// 商品业务属性
        /// </summary>
        [EntityPropertyDesc("商品业务属性")]
        public virtual ProductAttr ProductAttr { get; set; }

        /// <summary>
        /// 商品业务属性值
        /// </summary>
        [EntityPropertyDesc("商品业务属性值")]
        public virtual string Value { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [EntityPropertyDesc("是否使用")]
        public virtual bool IsUse { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityPropertyDesc("排序")]
        public virtual int Sort { get; set; }
    }
}
