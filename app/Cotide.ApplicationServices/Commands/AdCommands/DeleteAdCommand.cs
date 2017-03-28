//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteAdCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/23 19:02:40 
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

namespace Cotide.Tasks.Commands.AdCommands
{
    public class DeleteAdCommand : CommandBase
    {
        public readonly int Id;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        public DeleteAdCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
