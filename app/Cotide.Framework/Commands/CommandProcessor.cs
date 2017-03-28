//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CommandProcessor.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 11:36:25 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Domain.Commands;

namespace Cotide.Framework.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        public void Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            Validator.ValidateObject(command, new ValidationContext(command, null, null), true);

            var handler = ServiceLocator.Current.GetInstance<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
             
            handler.Handle(command); 
        }

        public TResult Process<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            Validator.ValidateObject(command, new ValidationContext(command, null, null), true);

            var handlers = ServiceLocator.Current.GetInstance<ICommandHandler<TCommand, TResult>>();
            if (handlers == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }


            return handlers.Handle(command);

        }

        public void Process<TCommand, TResult>(TCommand command, Action<TResult> resultHandler) where TCommand : ICommand
        {
            var result =
            Process<TCommand, TResult>(command);

            resultHandler(result);
        }
    }
}
