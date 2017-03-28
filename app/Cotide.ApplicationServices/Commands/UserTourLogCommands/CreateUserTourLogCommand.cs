//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateUserTourLogCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/9 0:01:16 
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
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.UserTourLogCommands
{
    /// <summary>
    /// 创建游览用户命令
    /// </summary>
    public class CreateUserTourLogCommand : CommandBase
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 游览用户ID
        /// </summary>
        public readonly int TourUserId;
         
        /// <summary>
        /// 最大记录游览人数 默认无限
        /// </summary>
        public int? MaxTourUserCount { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="tourUserId">游览用户ID</param>
        public CreateUserTourLogCommand(int userId,int tourUserId)
        {
            Guard.IsNotZeroOrNegative(userId, "userId");
            Guard.IsNotZeroOrNegative(tourUserId, "tourUserId");
            TourUserId = tourUserId;
            UserId = userId;
            MaxTourUserCount = null;
        }
    }
}
