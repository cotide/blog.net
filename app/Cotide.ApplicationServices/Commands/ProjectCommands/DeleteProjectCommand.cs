//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteProjectCommand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 11:59:19 
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

namespace Cotide.Tasks.Commands.ProjectCommands
{
    public class DeleteProjectCommand : CommandBase
    {
        public readonly int ProductId;

        public DeleteProjectCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
