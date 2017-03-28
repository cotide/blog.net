//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ShopType.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/29 11:56:25 
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
    /// 商品类型
    /// </summary>
    [EntityDesc("商品类型")]
    public class ProductType : Entity
    {
        /// <summary>
        /// 商品类型名称
        /// </summary>
        [EntityPropertyDesc("商品类型名称")]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        [EntityPropertyDesc("商品类型")]
        public virtual ProductType BaseProductType { get; set; }

        /// <summary>
        /// 商品扩展属性
        /// </summary>
        [EntityPropertyDesc("商品扩展属性")]
        public virtual IList<ProductAttr> ProductProerty { get; set; }

        /// <summary>
        /// 商品类型 路由 （针对一级菜单）
        /// </summary>
        [EntityPropertyDesc("商品类型 路由 （针对一级菜单）")]
        public virtual string UrlRouting { get; set; }

        /// <summary>
        /// 商品分类图标
        /// </summary>
        [EntityPropertyDesc("商品分类图标")]
        public virtual string Logo { get; set; }

        /// <summary>
        /// 级别 0为顶级
        /// </summary>
        [EntityPropertyDesc("级别 0为顶级")]
        public virtual int Level { get; set; }

        /// <summary>
        /// 前端是否显示
        /// </summary>
        [EntityPropertyDesc("前端是否显示")]
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary> 
        [EntityPropertyDesc("排序")]
        public virtual int Sort { get; set; }

        /// <summary>
        /// SEO - 商品标题
        /// </summary>
        [EntityPropertyDesc("SEO - 商品标题")]
        public virtual string Title { get; set; }

        /// <summary>
        /// SEO - 详细页描述
        /// </summary>
        [EntityPropertyDesc("SEO - 详细页描述")]
        public virtual string MetaDescription { get; set; }

        /// <summary>
        /// SEO - 详细页搜索关键字
        /// </summary>
        [EntityPropertyDesc("SEO - 详细页搜索关键字")]
        public virtual string MetaKeywords { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        [EntityPropertyDesc("商品列表")]
        public virtual IList<Product> Shops { get; set; }
    }
}
