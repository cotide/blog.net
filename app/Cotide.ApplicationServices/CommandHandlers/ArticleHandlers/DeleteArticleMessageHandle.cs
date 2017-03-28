using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Exceptions;
using Cotide.Tasks.Commands.ArticleCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.ArticleHandlers
{
    public class DeleteArticleMessageHandle : ICommandHandler<DeleteArticleMessageCommand>
    {
        private readonly IArticleMessageRepository _articleMessageRepository;
        private readonly IArticleRepository _articleRepository;

        public DeleteArticleMessageHandle(
            IArticleMessageRepository articleMessageRepository, 
            IArticleRepository articleRepository)
        {
            _articleMessageRepository = articleMessageRepository;
            _articleRepository = articleRepository;
        }

        public void Handle(DeleteArticleMessageCommand command)
        {   
            var articleMessage = _articleMessageRepository.Get(command.ArticleMessageId);
            Guard.IsNotNull(articleMessage, "articleMessage");
             
            if (articleMessage.Article.User.Id != command.UserId)
            {
                throw new BusinessException("不允许删除其他用户的留言信息");
            }  
            var part = new  Dictionary<string, object> {{"@ArticleMessageID", command.ArticleMessageId}};
            _articleMessageRepository.ExecuteProcedure("Cotide_DeleteArticleMessage", part);
        }
    }
}
