//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：Shop.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/7 14:21:01 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 商品
    /// </summary>
    [EntityDesc("商品")]
    public class Product : Entity, IAggregateRoot
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [EntityPropertyDesc("商品名称")]
        public virtual string ProductName { get; set; }

        /// <summary>
        /// 商品编码 
        /// </summary>
        [EntityPropertyDesc("商品编码")]
        public virtual string ProductNo { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        [EntityPropertyDesc("商品类型")]
        public virtual ProductType ProductType { get; set; }

        /// <summary>
        /// 商品标签
        /// </summary>
        [EntityPropertyDesc("商品标签")]
        public virtual IList<ProductTag> ProductTags { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [EntityPropertyDesc("商品图片")]
        public virtual IList<ProductImg> ProductImgs { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [EntityPropertyDesc("品牌")]
        public virtual IList<Brand> Brands { get; set; }

        ///// <summary>
        ///// 商品扩展属性Xml
        ///// </summary>
        //public virtual string ProductAttrXml { get; set; }

        /// <summary>
        /// 商品扩展属性
        /// </summary>
        [EntityPropertyDesc("商品扩展属性")]
        public virtual IList<ProductProerty> ProductProertys { get; set; }

        /// <summary>
        /// 简单描述
        /// </summary>
        [EntityPropertyDesc("简单描述")]
        public virtual string SimpleDescription { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [EntityPropertyDesc("商品描述")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        [EntityPropertyDesc("商品单价")]
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 商品成本价
        /// </summary>
        [EntityPropertyDesc("商品成本价")]
        public virtual decimal CostPrice { get; set; }

        /// <summary>
        /// 商品折扣数 1为全额 
        /// </summary>
        [EntityPropertyDesc("商品折扣数 1为全额")]
        public virtual double DiscountCount { get; set; }

        /// <summary>
        /// 商品库存数量 
        /// </summary>
        [EntityPropertyDesc("商品库存数量")]
        public virtual int ShopTotal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityPropertyDesc("备注")]
        public virtual string Remark { get; set; }

        /// <summary>
        /// SEO - 商品标题
        /// </summary>
        [EntityPropertyDesc("SEO - 商品标题")]
        public virtual string Title { get; set; }

        /// <summary>
        /// SEO - 详细页描述
        /// </summary>
        [EntityPropertyDesc("SEO  - 详细页描述")]
        public virtual string MetaDescription { get; set; }

        /// <summary>
        /// SEO - 详细页搜索关键字
        /// </summary>
        [EntityPropertyDesc("SEO  - 详细页搜索关键字")]
        public virtual string MetaKeywords { get; set; }
    }
}
