//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateAdCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/23 19:02:02 
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
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.AdCommands
{
    public class CreateAdCommand : CommandBase
    {
        /// <summary>
        /// 广告名称
        /// </summary> 
        public readonly string AdName;
         
        /// <summary>
        /// 广告描述
        /// </summary> 
        public readonly string AdDesc;
         
        /// <summary>
        /// 是否显示
        /// </summary> 
        public readonly bool IsShow;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;
         
        /// <summary>
        /// 广告类型
        /// </summary> 
        public AdType AdType { get; set; }
         
        /// <summary>
        /// 广告图片
        /// </summary> 
        public string AdImg { get; set; }

        /// <summary>
        /// 广告小图片
        /// </summary>
        public string SmallImg { get; set; }

        /// <summary>
        /// 缩略广告图片
        /// </summary>
        public string StandardAdImg { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        public CreateAdCommand(
            string adName,
            string adDesc, 
            bool isShow, 
            int userId)
        {
            Guard.IsNotNullOrEmpty(adName, "adName");
            Guard.IsNotZeroOrNegative(userId, "userId");
            AdName = adName;
            AdDesc = adDesc; 
            IsShow = isShow;
            UserId = userId;
            AdType = Cotide.Domain.Enum.AdType.Home;
        }
    }
}
