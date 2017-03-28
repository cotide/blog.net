//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminRoleDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/16 22:39:53 
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
    public class AdminRoleDto
    {
        /// <summary>
        /// 角色名称
        /// </summary> 
        public string RoleName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 角色权限列表
        /// </summary> 
        public int[] AdminPowerIds { get; set; }

        /// <summary>
        /// 角色菜单列表
        /// </summary> 
        public int[] AdminMenuIds { get; set; }
    }
}
