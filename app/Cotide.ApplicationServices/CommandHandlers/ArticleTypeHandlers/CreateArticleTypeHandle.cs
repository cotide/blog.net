//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateArticleTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/1/10 22:54:15 
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
using Cotide.Tasks.Commands.ArticleTypeCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTypeHandlers
{
    public class CreateArticleTypeHandle : ICommandHandler<CreateArticleTypeCommand>
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IArticleTypeRepository _iArticleTypeRepository;

        public CreateArticleTypeHandle(
            IUserRepository iUserRepository,
            IArticleTypeRepository iArticleTypeRepository)
        {
            _iUserRepository = iUserRepository;
            _iArticleTypeRepository = iArticleTypeRepository;
        }


        public void Handle(CreateArticleTypeCommand command)
        {
            var user = _iUserRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");

            var query = (from a in _iArticleTypeRepository.FindAll()
                         let u = a.User
                         where u.Id == command.UserId
                               && a.TypeName == command.TypeName
                         select a);

            if (query.Count() > 0)
            {
                throw new BusinessException("已经存在相同的文章类别名称!");
            }

            _iArticleTypeRepository.Save(new ArticleType()
            {
                TypeName = command.TypeName,
                IsShow = command.IsShow,
                User = user,
                CreateDate = DateTime.Now,
                LastDateTime = DateTime.Now
            });
        }

    }
}
