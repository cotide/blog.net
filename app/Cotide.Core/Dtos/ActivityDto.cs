//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/19 11:05:46 
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
    /// 活动Dto
    /// </summary>
    public class ActivityDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 活动图片
        /// </summary> 
        public string ActivityImg { get; set; }

        /// <summary>
        /// 活动图片(标准图) 210 X 132
        /// </summary> 
        public string StandardActivityImg { get; set; }

        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string ContentDesc { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 集合地方
        /// </summary>
        public string GatherPlace { get; set; }

        /// <summary>
        /// 主办方
        /// </summary>
        public string Organizers { get; set; }

        /// <summary>
        /// 活动费用
        /// </summary>
        public decimal ActivityMoney { get; set; }

        /// <summary>
        /// 活动类型ID
        /// </summary>
        public int ActivityTypeId { get; set; }

        /// <summary>
        /// 活动类型名称
        /// </summary>
        public string ActivityTypeName { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityState ActivityState { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 发布用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 发布用户帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发布用户昵称
        /// </summary>
        public string RealName { get; set; }
    }
}
