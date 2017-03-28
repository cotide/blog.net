//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityOrderStateEnum.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/5 0:25:42 
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
    /// 活动订单状态
    /// </summary>
    public enum ActivityOrderState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        Untreated,

        /// <summary>
        /// 处理中
        /// </summary>
        [Description("处理中")]
        Processing, 

        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        End
    }
}
