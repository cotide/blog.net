//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminLevelEnum.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/4 16:50:27 
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
    /// 管理员等级
    /// </summary>
    public enum AdminLevel
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        Super = 0,

        /// <summary>
        /// 网点管理员
        /// </summary>
        [Description("网点管理员")]
        ShopAdmin = 1,

        /// <summary>
        /// 失踪周末管理员
        /// </summary>
        [Description("失踪周末管理员")]
        ShizhongSiteAdmin = 2
    }
}
