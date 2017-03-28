//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductProerty.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/17 11:03:03 
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
    /// 商品属性
    /// </summary>
     [EntityDesc("商品属性")]
    public class ProductProerty : Entity
    {
        /// <summary>
        /// 商品扩展属性
        /// </summary>
        [EntityPropertyDesc("商品扩展属性")]
        public virtual ProductAttr ProductAttr { get; set; }

        /// <summary>
        /// 商品扩展属性值
        /// </summary>
        [EntityPropertyDesc("商品扩展属性值")]
        public virtual IList<ProductAttrValue> ProductAttrValues { get; set; } 
    }
}
