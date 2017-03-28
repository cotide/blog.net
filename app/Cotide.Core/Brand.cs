//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：Brand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/21 14:09:39 
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
    /// 品牌
    /// </summary>
    [EntityDesc("品牌")]
    public class Brand : Entity
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        [EntityPropertyDesc("品牌名称")]
        public virtual string BrandName { get; set; }

        /// <summary>
        /// 品牌Logo
        /// </summary>
        [EntityPropertyDesc("品牌Logo")]
        public virtual string BrandLogo { get; set; }

        /// <summary>
        /// 品牌官方地址
        /// </summary>
        [EntityPropertyDesc("品牌官方地址")]
        public virtual string BrandWebSite { get; set; }

        /// <summary>
        /// 品牌描述
        /// </summary>
        [EntityPropertyDesc("品牌描述")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityPropertyDesc("排序")]
        public virtual int Sort { get; set; }
    }
}
