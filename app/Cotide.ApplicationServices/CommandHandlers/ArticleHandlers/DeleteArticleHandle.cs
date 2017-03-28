//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteArticleHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 22:46:44 
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
using Cotide.Tasks.Commands.ArticleCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleHandlers
{
    public class DeleteArticleHandle : ICommandHandler<DeleteArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleMessageRepository _articleMessageRepository;

        public DeleteArticleHandle(IArticleRepository articleRepository, IArticleMessageRepository articleMessageRepository)
        {
            _articleRepository = articleRepository;
            _articleMessageRepository = articleMessageRepository;
        }

        public void Handle(DeleteArticleCommand command)
        {
            var article = _articleRepository.Load(command.ArticleId);
            Guard.IsNotNull(article, "找不到文章");
            if (article.User.Id != command.UserId)
            {
                throw new PowerException("非法操作");
            }
            var part = new  Dictionary<string, object>();
            part.Add("@ArticleId", command.ArticleId);
            _articleMessageRepository.ExecuteProcedure("Cotide_DeleteArticle",part); 
        }
         
    }
}
