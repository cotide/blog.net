//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateArticleTagHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/15 23:44:03 
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
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleTagCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTagHandlers
{
    public class CreateArticleTagHandle : ICommandHandler<CreateArticleTagCommand>
    {
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly IUserRepository _userRepository;

        public CreateArticleTagHandle(
            IArticleTagRepository articleTagRepository,
            IUserRepository userRepository
            )
        {
            _articleTagRepository = articleTagRepository;
            _userRepository = userRepository;
        }


        public void Handle(CreateArticleTagCommand command)
        {
            var user = _userRepository.Load(command.UserId);
            Guard.IsNotNull(user, "user");

            var query = (from a in _articleTagRepository.FindAll()
                         let u = a.User
                         where u.Id == command.UserId
                               && a.TagName == command.TagName
                         select a);

            if (query.Count() > 0)
            {
                throw new BusinessException("已经存在相同的文章标签名称!");
            }

            _articleTagRepository.Save(new ArticleTag()
            {
                TagName = command.TagName,
                IsShow = command.IsShow,
                User = user,
                CreateDate = DateTime.Now,
                LastDateTime = DateTime.Now
            });
        }


    }
}
