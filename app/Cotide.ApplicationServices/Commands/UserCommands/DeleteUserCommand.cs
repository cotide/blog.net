//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteUserCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 12:02:05 
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
using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.UserCommands
{
    /// <summary>
    /// 删除用户命令
    /// </summary>
    public class DeleteUserCommand : CommandBase
    {
        public readonly int[] UserIds;
        public DeleteUserCommand(params int[] userIds)
        {
            Guard.IsNotNull(userIds, "userIds");
            UserIds = userIds;
        }
    }
}
