//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserTourLog.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/8 22:58:44 
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
    [EntityDesc("用户游览记录")]
    public class UserTourLog : Entity, IAggregateRoot
    {

        /// <summary>
        /// 当前用户
        /// </summary>
        [EntityPropertyDesc("当前用户")]
        public virtual User User { get; set; }

        /// <summary>
        /// 游览用户
        /// </summary>
        [EntityPropertyDesc("游览用户")]
        public virtual User TourUser { get; set; }


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
