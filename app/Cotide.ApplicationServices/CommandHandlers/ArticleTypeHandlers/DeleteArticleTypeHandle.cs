//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteArticleTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 23:01:17 
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
using Cotide.Tasks.Commands.ArticleTypeCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTypeHandlers
{
    public class DeleteArticleTypeHandle : ICommandHandler<DeleteArticleTypeCommand>
    {
        private readonly IArticleTypeRepository _iArticleTypeRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;

        #region IQC注入
        public DeleteArticleTypeHandle(
            IArticleTypeRepository articleTypeRepository,
            IArticleRepository articleRepository, IUserRepository userRepository)
        {
            _iArticleTypeRepository = articleTypeRepository;
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }

        #endregion

        public void Handle(DeleteArticleTypeCommand command)
        {
            var user = _userRepository.Get(command.CurrentUserId);
            Guard.IsNotNull(user, "user");
            if (!user.UserRole.HasFlag(UserRole.Admin))
            {
                throw new PowerException("当前用户无权限删除文章分类");
            }
             
            var articleType = _iArticleTypeRepository.Load(command.ArticleTypeId);
            Guard.IsNotNull(articleType, "articleType");
            var article = (from a in _articleRepository.FindAll()
                           let at = a.ArticleType
                           where at.Id == articleType.Id
                           select a);
            if(article.Count()>0)
            {
                throw  new BusinessException("文章分类下有文章，删除后才能进行移除");
            }

            _iArticleTypeRepository.Delete(articleType);
        }

    }
}
