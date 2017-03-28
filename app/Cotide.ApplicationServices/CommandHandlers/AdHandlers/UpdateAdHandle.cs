//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateAdHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/24 22:45:41 
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
using Cotide.Tasks.Commands.AdCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.AdHandlers
{
    public class UpdateAdHandle : ICommandHandler<UpdateAdCommand>
    {
        protected readonly IUserRepository UserRepository;
        protected readonly IAdRepository AdRepository;

        public UpdateAdHandle(
            IUserRepository userRepository,
            IAdRepository adRepository)
        {
            UserRepository = userRepository;
            AdRepository = adRepository;
        }

        public void Handle(UpdateAdCommand command)
        {
            var user = UserRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");
            if (!user.UserRole.HasFlag(UserRole.Admin))
            {
                throw new PowerException("权限不足");
            }
            var ad = AdRepository.Get(command.Id);
            Guard.IsNotNull(ad, "ad");
            ad.AdDesc = command.AdDesc;
            ad.AdImg = command.AdImg;
            ad.AdName = command.AdName;
            ad.IsShow = command.IsShow;
            ad.Sort = command.Sort;
            ad.SmallAdImg = command.SmallImg;
            ad.StandardAdImg = command.StandardAdImg;
             
            if (command.Sort == null || command.Sort <= 0)
            {
                ad.Sort = GetAdMaxSort();
            }

            AdRepository.Update(ad);
        }


        private int GetAdMaxSort()
        {
            var resut = (from adQuery in AdRepository.FindAll()
                         orderby adQuery.Sort descending
                         select adQuery).FirstOrDefault();

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
