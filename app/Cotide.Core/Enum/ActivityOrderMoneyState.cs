//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityOrderMoneyStateEnum.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/5 0:29:39 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 活动订单付费状态
    /// </summary>
    public enum ActivityOrderMoneyState
    { 
        /// <summary>
        /// 未付
        /// </summary>
        [Description("未付")]
        Unpaid,

        /// <summary>
        /// 已付
        /// </summary>
        [Description("已付")]
        Paid
    }
}
