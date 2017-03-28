//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateProductCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/11/13 23:51:28 
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
using Cotide.Domain;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.ProductCommands
{
    /// <summary>
    /// 创建产品命令
    /// </summary>
    public class CreateProductCommand : CommandBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public readonly string ShopName;

        /// <summary>
        /// 商品类型
        /// </summary>
        public readonly int ProductType;
         
        /// <summary>
        /// 创建产品构造函数
        /// </summary>
        /// <param name="shopName">商品名称</param>
        /// <param name="productType">商品类型</param>
        /// <param name="description">商品描述</param>
        public CreateProductCommand(
            string shopName,
            int productType, 
            string description)
        {
            Guard.IsNotNullOrEmpty(shopName, "shopName");
            ShopName = shopName;
            Guard.IsNotZeroOrNegative(productType, "productType");
            ProductType = productType; 
            ShopTags=new List<int>();
            Price = 0;
            CostPrice = 0;
            DiscountCount = 1;
            ShopTotal = 0;
        } 

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
