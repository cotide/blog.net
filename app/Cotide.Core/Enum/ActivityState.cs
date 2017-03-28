//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityStateEnum.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/4 16:59:04 
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
    /// 活动状态
    /// </summary>
    public enum ActivityState
    {
        /// <summary>
        /// 招募中
        /// </summary>
        [Description("招募中")]
        Recruit = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        Conduct = 1,

        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        End = 2,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Stop = 3
    }
}
