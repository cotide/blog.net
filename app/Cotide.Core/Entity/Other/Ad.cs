//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：Ad.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/2/19 18:24:21 
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
    /// 广告
    /// </summary>
    [EntityDesc("广告")]
    public class Ad : Entity, IAggregateRoot
    {
        /// <summary>
        /// 广告名称
        /// </summary>
        [EntityPropertyDesc("广告名称")]
        public virtual string AdName { get; set; }


        /// <summary>
        /// 广告类型
        /// </summary>
        [EntityPropertyDesc("广告类型")]
        public virtual AdType AdType { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        [EntityPropertyDesc("广告描述")]
        public virtual string AdDesc { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary>
        [EntityPropertyDesc("广告图片")]
        public virtual string AdImg { get; set; }

        /// <summary>
        /// 广告图片(小图) 50 X 50
        /// </summary>
        [EntityPropertyDesc("广告图片(小图) 50 X 50")]
        public virtual string SmallAdImg { get; set; }

        /// <summary>
        /// 广告图片(标准图) 940 X 450
        /// </summary>
        [EntityPropertyDesc("用户头像(标准图) 940 X 450")]
        public virtual string StandardAdImg { get; set; }
         
        /// <summary>
        /// 是否显示
        /// </summary>
        [EntityPropertyDesc("是否显示")]
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityPropertyDesc("排序")]
        public virtual int? Sort { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary> 
        [EntityPropertyDesc("创建时间")]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary> 
        [EntityPropertyDesc("最后修改时间")]
        public virtual DateTime? LastDateTime { get; set; }

    }
}
