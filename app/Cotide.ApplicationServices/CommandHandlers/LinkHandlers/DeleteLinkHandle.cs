//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteLinkHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 11:21:54 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.LinkCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.LinkHandlers
{
    public class DeleteLinkHandle : ICommandHandler<DeleteLinkCommand>
    {
          private readonly IUserRepository _userRepository;
        private readonly ILinkRepository _linkRepository;

        #region IOC注入
        public DeleteLinkHandle(IUserRepository userRepository, ILinkRepository linkRepository)
                {
                    _userRepository = userRepository;
                    _linkRepository = linkRepository;
                }
        #endregion

        #region Implementation of ICommandHandler<in DeleteLinkCommand,out int>

        public void Handle(DeleteLinkCommand command)
        {
            var link = _linkRepository.Get(command.LinkId);
            Guard.IsNotNull(link, "link");
            _linkRepository.Delete(link);
        }

        #endregion
    }
}
