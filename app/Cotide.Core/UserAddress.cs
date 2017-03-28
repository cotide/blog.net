//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserAddress.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/29 17:16:44 
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
    /// 用户地址
    /// </summary>
     [EntityDesc("用户地址")]
    public class UserAddress: Entity
    { 
        /// <summary>
        /// 所属用户
        /// </summary>
       [EntityPropertyDesc("所属用户")]
        public virtual User User { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [EntityPropertyDesc("国家")]
        public virtual string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [EntityPropertyDesc("省份")]
        public virtual string Provice { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [EntityPropertyDesc("城市")]
        public virtual string City { get; set; }
         
        /// <summary>
        /// 邮政编码
        /// </summary>
        [EntityPropertyDesc("邮政编码")]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [EntityPropertyDesc("地址")]
        public virtual string Address { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [EntityPropertyDesc("是否默认")]
        public virtual bool IsDefault { get; set; }

    }
}
