//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminPowerDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/24 20:34:37 
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
    /// <summary>
    /// 管理员权限Dto
    /// </summary>
    public class AdminPowerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary> 
        public string PowerName { get; set; }

        /// <summary>
        /// 对应的Action
        /// </summary> 
        public string ActionName { get; set; }

        /// <summary>
        /// 对应的Controller
        /// </summary> 
        public string ControllerName { get; set; }
    }
}
