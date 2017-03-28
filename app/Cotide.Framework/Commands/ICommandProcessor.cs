//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ICommandProcessor.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 11:35:51 
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

namespace Cotide.Framework.Commands
{
    public interface ICommandProcessor
    {
        void Process<TCommand>(TCommand command) where TCommand : ICommand;

        TResult Process<TCommand, TResult>(TCommand command) where TCommand : ICommand;

        void Process<TCommand, TResult>(TCommand command, Action<TResult> resultHandler) where TCommand : ICommand;
    }
}
