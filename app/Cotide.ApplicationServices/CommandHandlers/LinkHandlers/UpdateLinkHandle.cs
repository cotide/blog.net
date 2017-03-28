//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateLinkHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 11:22:16 
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
    public class UpdateLinkHandle : ICommandHandler<UpdateLinkCommand>
    {
        protected readonly IUserRepository UserRepository;
        protected readonly ILinkRepository LinkRepository;
        protected readonly ILinkTypeRepository LinkTypeRepository;

        #region IOC注入
        public UpdateLinkHandle(
            IUserRepository userRepository, 
            ILinkRepository linkRepository, 
            ILinkTypeRepository linkTypeRepository)
                {
                    UserRepository = userRepository;
                    LinkRepository = linkRepository;
                    LinkTypeRepository = linkTypeRepository;
                }
        #endregion

        public void Handle(UpdateLinkCommand  command)
        {
            var link = LinkRepository.Get(command.LinkId);
            Guard.IsNotNull(link, "link");
            if (!string.IsNullOrEmpty(command.Description))
            {
                link.Description = command.Description;
            }
            if (!string.IsNullOrEmpty(command.Img))
            {
                link.Img = command.Img;
            }
            if (command.IsShow != null)
            {
                link.IsShow = (bool)command.IsShow;
            }
            if (command.LinkTypeId != null)
            {

                var linkType = LinkTypeRepository.Get((int)command.LinkTypeId);
                Guard.IsNotNull(linkType, "linkType");
                link.LinkType = linkType;
            }

            if (!string.IsNullOrEmpty(command.LinkTxt))
            {
                link.LinkTxt = command.LinkTxt;
            }
            if (!string.IsNullOrEmpty(command.LinkUrl))
            {
                link.LinkUrl = command.LinkUrl;
            }

            link.LastDateTime = DateTime.Now;
            LinkRepository.SaveOrUpdate(link);
        }
         
    }
}
