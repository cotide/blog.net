//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductAttr.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 16:19:24 
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
using Cotide.Domain.Enum;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 商品业务属性
    /// </summary>
    [EntityDesc("商品业务属性")]
    public class ProductAttr : Entity
    {
        /// <summary>
        /// 属性值标记
        /// </summary>
        [EntityPropertyDesc("属性值标记")]
        public virtual string Key { get; set; }

        /// <summary>
        /// 属性显示名称
        /// </summary>
        [EntityPropertyDesc("属性显示名称")]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [EntityPropertyDesc("字段名称")]
        public virtual string FieldName { get; set; }

        /// <summary>
        /// 所属商品类型
        /// </summary>
        [EntityPropertyDesc("所属商品类型")]
        public virtual IList<ProductType> ProductTypes { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [EntityPropertyDesc("属性类型")]
        public virtual ProductAttrType AttrType { get; set; }

        /// <summary>
        /// 商品属性值
        /// </summary>
        [EntityPropertyDesc("商品属性值")]
        public virtual IList<ProductAttrValue> ProductAttrValues { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityPropertyDesc("排序")]
        public virtual int Sort { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [EntityPropertyDesc("是否使用")]
        public virtual bool IsUse { get; set; }
    }

}
