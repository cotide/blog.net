//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ShopTag.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/29 12:00:05 
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
    /// 商品标签
    /// </summary>
    [EntityDesc("商品标签")]
    public class ProductTag : Entity
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [EntityPropertyDesc("标签名称")]
        public virtual string TagName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityPropertyDesc("排序")]
        public virtual int Sort { get; set; }

        /// <summary>
        /// 前端是否显示
        /// </summary>
        [EntityPropertyDesc("前端是否显示")]
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        [EntityPropertyDesc("商品列表")]
        public virtual IList<Product> Products { get; set; }
    }
}
