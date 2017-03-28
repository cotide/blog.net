//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductTypeDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/22 23:10:03 
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

namespace Cotide.Domain.Dtos
{
    /// <summary>
    /// 商品类型Dto
    /// </summary>
    public class ProductTypeDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int BaseProductType { get; set; }

        /// <summary>
        /// 商品类型 路由 （针对一级菜单）
        /// </summary>
        public string UrlRouting { get; set; }

        /// <summary>
        /// 商品分类图标
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 级别 0为顶级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 前端是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// SEO - 商品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// SEO - 详细页描述
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// SEO - 详细页搜索关键字
        /// </summary>
        public string MetaKeywords { get; set; }

    }
}
