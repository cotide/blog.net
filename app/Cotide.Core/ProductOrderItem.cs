//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductOrderItem.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:50:08 
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
    /// 商品订单项
    /// </summary>
    [EntityDesc("商品订单项")]
    public class ProductOrderItem : Entity 
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [EntityPropertyDesc("商品ID")]
        public virtual int ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [EntityPropertyDesc("商品名称")]
        public virtual string ProductName { get; set; }

        /// <summary>
        /// 订单项编码 
        /// </summary>
        [EntityPropertyDesc("订单项编码")]
        public virtual string OrderItemNo { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        [EntityPropertyDesc("商品单价")]
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [EntityPropertyDesc("商品数量")]
        public virtual int Count { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        [EntityPropertyDesc("商品总价")]
        public virtual decimal SumPrice { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [EntityPropertyDesc("订单状态")]
        public virtual ProductOrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityPropertyDesc("备注")]
        public virtual string Remark { get; set; }

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

    }
}
