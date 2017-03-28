//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteAdHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/24 23:10:17 
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
using Cotide.Domain.Enum;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.AdCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.AdHandlers
{
    public class DeleteAdHandle : ICommandHandler<DeleteAdCommand>
    {
        protected readonly IUserRepository UserRepository;
        protected readonly IAdRepository AdRepository;

        public DeleteAdHandle(
            IAdRepository adRepository, 
            IUserRepository userRepository)
        {
            AdRepository = adRepository;
            UserRepository = userRepository;
        }


        public void Handle(DeleteAdCommand command)
        {
            var user = UserRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");
            if (!user.UserRole.HasFlag(UserRole.Admin))
            {
                throw new PowerException("权限不足");
            }

             var ad = AdRepository.Get(command.Id);
            Guard.IsNotNull(ad, "ad");
            AdRepository.Delete(ad);
        }
         
    }
}
