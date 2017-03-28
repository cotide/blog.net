//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateAdHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/24 22:36:49 
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
using Cotide.Domain;
using Cotide.Framework.Exceptions;
using Cotide.Tasks.Commands.AdCommands;
using SharpArch.Domain.Commands;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Domain.Enum;

namespace Cotide.Tasks.CommandHandlers.AdHandlers
{
    public class CreateAdHandle : ICommandHandler<CreateAdCommand>
    {
        protected readonly IUserRepository UserRepository;
        protected readonly IAdRepository AdRepository;

        public CreateAdHandle(
            IUserRepository userRepository,
            IAdRepository adRepository)
        {
            UserRepository = userRepository;
            AdRepository = adRepository;
        }

        public void Handle(CreateAdCommand command)
        {
            var user = UserRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");
            if(!user.UserRole.HasFlag(UserRole.Admin))
            {
                throw new PowerException("权限不足");
            }
            var sort = command.Sort;
            if (command.Sort==null || command.Sort <= 0)
            {
                sort= GetAdMaxSort();
            }
            AdRepository.Save(new Ad()
                                  {
                                      AdDesc = command.AdDesc,
                                      AdImg = command.AdImg,
                                      StandardAdImg = command.StandardAdImg,
                                      AdName = command.AdName,
                                      AdType = command.AdType,
                                      IsShow = command.IsShow,
                                      SmallAdImg = command.SmallImg ,
                                      Sort = sort
                                  });
        }

        private int GetAdMaxSort()
        {
            var resut =  (from adQuery in AdRepository.FindAll()
                         orderby adQuery.Sort descending 
                         select  adQuery).FirstOrDefault();

            if (resut != null)
            {
                if (resut.Sort != null)
                    return Convert.ToInt32(resut.Sort);
                return 1;
            }
            return 1; 
        }
    }
}
