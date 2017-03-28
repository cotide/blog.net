//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminDetailDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/26 17:26:15 
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
    public class AdminDetailDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary> 
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary> 
        public string Mobile { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否锁定 
        /// </summary> 
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public int[] AdminRoles { get; set; } 
    }
}
