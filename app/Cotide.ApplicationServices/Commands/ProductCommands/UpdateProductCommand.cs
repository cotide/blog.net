//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateProductCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/11/13 23:52:05 
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
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.ProductCommands
{
    /// <summary>
    /// 更新产品命令
    /// </summary>
    public class UpdateProductCommand : CommandBase
    {
        public readonly int Id;

        public UpdateProductCommand(int id)
        {
            Guard.IsNotZeroOrNegative(id,"id");
            Id = id;
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品标签
        /// </summary>
        public IList<int> ShopTags { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品成本价
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 商品折扣数 1为全额 
        /// </summary>
        public double DiscountCount { get; set; }

        /// <summary>
        /// 商品库存数量 
        /// </summary>
        public int ShopTotal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
