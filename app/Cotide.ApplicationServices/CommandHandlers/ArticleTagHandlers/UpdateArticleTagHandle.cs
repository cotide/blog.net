//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateArticleTagHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/15 23:48:22 
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
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleTagCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTagHandlers
{
    public class UpdateArticleTagHandle : ICommandHandler<UpdateArticleTagCommand>
    {
        private readonly IArticleTagRepository _articleTagRepository; 

        public UpdateArticleTagHandle(
            IArticleTagRepository articleTagRepository 
            )
        {
            _articleTagRepository = articleTagRepository; 
        }
         
        public void Handle(UpdateArticleTagCommand command)
        {
            var articleTag = _articleTagRepository.Load(command.ArticleTagId);
            Guard.IsNotNull(articleTag, "articleTag");

            if (articleTag.User.Id != command.CurrentUserId)
            {
                throw new BusinessException("非法操作");
                return;
            }

            var isHaveArticleTag = (from query in _articleTagRepository.FindAll()
                                     where query.TagName == command.TagName
                                           && query.Id != articleTag.Id
                                     select query);
            if (isHaveArticleTag.Count() > 0)
            {
                throw new BusinessException("已经存在相同的文章标签名称!");
            } 

            if (command.IsShow != null)
            {
                articleTag.IsShow = (bool)command.IsShow;
            }
            if (!string.IsNullOrEmpty(command.TagName))
            {
                articleTag.TagName = command.TagName.Trim();
            }
            articleTag.LastDateTime = DateTime.Now;
            _articleTagRepository.SaveOrUpdate(articleTag);
        }

    }
}
