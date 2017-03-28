//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：SysMenuDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/4 23:20:09 
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
using Cotide.Domain.Enum;

namespace Cotide.Domain.Dtos
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SysMenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary> 
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary> 
        public string MenuNo { get; set; }

        /// <summary>
        /// 菜单父节点编码
        /// </summary> 
        public string MenuParentNo { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary> 
        public int MenuOrder { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary> 
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary> 
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary> 
        public string MenuIcon { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary> 
        public bool IsVisible { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary> 
        public int Level { get; set; }
         
    }
}
