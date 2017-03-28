//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserRole.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/16 22:47:38 
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
    /// 用户角色
    /// </summary>  
    public enum UserRole
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User = 0,
        /// <summary>
        /// 失踪周末管理员
        /// </summary>
        [Description("失踪周末管理员")]
        Admin = 1
    }
}
