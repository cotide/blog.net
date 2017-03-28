//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityTypeDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/19 11:05:39 
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
    /// 活动类别Dto
    /// </summary>
    public class ActivityTypeDto
    {
        public int Id { get; set; }


        /// <summary>
        /// 活动类别名称
        /// </summary>  
        public string TypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
         
        /// <summary>
        /// 所属用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
    }
}
