//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ShopOrder.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/29 16:24:55 
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
using Cotide.Domain.Enum;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 商品订单
    /// </summary>
    [EntityDesc("商品订单")]
    public class ProductOrder : Entity, IAggregateRoot
    {

        /// <summary>
        /// 订单项编码 
        /// </summary>
        [EntityPropertyDesc("订单项编码")]
        public virtual string OrderNo { get; set; }

        /// <summary>
        /// 下订单用户
        /// </summary>
        [EntityPropertyDesc("下订单用户")]
        public virtual User User { get; set; }

        /// <summary>
        /// 商品订单项列表
        /// </summary>
        [EntityPropertyDesc("商品订单项列表")]
        public virtual IList<ProductOrderItem> ProductOrderItems { get; set; }

        /// <summary>
        /// 订单付费状态
        /// </summary>
        [EntityPropertyDesc("订单付费状态")]
        public virtual ProductOrderPayStatus OrderPayStatus { get; set; }

        /// <summary>
        /// 买家联系电话号码
        /// </summary>
        [EntityPropertyDesc("买家联系电话号码")]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 买家联系地址
        /// </summary>
        [EntityPropertyDesc("买家联系地址")]
        public virtual string Address { get; set; }

        /// <summary>
        /// 商品总数量
        /// </summary>
        [EntityPropertyDesc("商品总数量")]
        public virtual int SumCount { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        [EntityPropertyDesc("商品总价")]
        public virtual decimal SumPrice { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityPropertyDesc("创建时间")]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [EntityPropertyDesc("最后更新时间")]
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 下单IP
        /// </summary>
        [EntityPropertyDesc("下单IP")]
        public virtual string Ip { get; set; }
    }
}
