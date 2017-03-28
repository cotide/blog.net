//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateLinkHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 11:21:28 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.LinkCommands;
using Cotide.Tasks.Commands.UserCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.LinkHandlers
{
    public class CreateLinkHandle : ICommandHandler<CreateLinkCommand>
    {
        
        protected readonly IUserRepository UserRepository;
        protected readonly ILinkRepository LinkRepository;
        protected readonly ILinkTypeRepository LinkTypeRepository;

        #region IOC注入
        public CreateLinkHandle(
            IUserRepository userRepository, 
            ILinkRepository linkRepository,
            ILinkTypeRepository linkTypeRepository)
                {
                    UserRepository = userRepository;
                    LinkRepository = linkRepository;
                    LinkTypeRepository = linkTypeRepository;
                }
        #endregion
        
        public void Handle(CreateLinkCommand command)
        {
            var user = UserRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");

            var linkType = LinkTypeRepository.Get(command.LinkTypeId);
            Guard.IsNotNull(linkType, "linkType");


            var link = new Link()
            {
                Description = command.Description,
                Img = command.Img,
                IsShow = command.IsShow,
                LinkTxt = command.LinkTxt,
                LinkUrl = command.LinkUrl,
                User = user,
                LinkType =linkType,
                CreateDate = DateTime.Now,
                LastDateTime = DateTime.Now
            };
            LinkRepository.Save(link);
        }
         
    }
}
